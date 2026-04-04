using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    [SerializeField] bool debugMessage = true;

    [SerializeField] TextMeshProUGUI activeItem;

    [Space]
    [SerializeField] AudioSource itemsAudio;
    [SerializeField] AudioSource panelAudio;

    [Space]
    [SerializeField] List<ItemScriptable> debugItems;

    InventoryManager inventoryMan;
    ItemDatabase database;
    GameManager gameMan;
    UIManager uiMan;

    private void Start()
    {
        inventoryMan = InventoryManager.Instance;
        database = inventoryMan.itemDatabase;
        gameMan = GameManager.Instance;
        uiMan = UIManager.Instance;

        AddDebugItems();
    }

    private void Update()
    {
        if (inventoryMan.activeItem != null)
            activeItem.text = "Active item: " + inventoryMan.activeItem.itemName;
        else
            activeItem.text = "No active item";
    }

    void AddDebugItems()
    {
        foreach (var item in debugItems)
        {
            TryAddItem(item.itemName);
        }
    }

    public bool TryAddItem(string item)
    {
        if (item == null)
        {
            DebugMessage("Item does not exist.");
            return false;
        }

        foreach (var item_ in gameMan.gameData.inventoryData.items)
        {
            if (item_.Value == null)
            {
                DebugMessage("Adding item: " + item);
                gameMan.gameData.inventoryData.items[item_.Key] = item;
                return true;
            }
        }

        DebugMessage("Inventory full");
        return false;
    }

    public bool TryCombineItems(int slot, out ItemScriptable comboItem)
    {
        ItemScriptable item = inventoryMan.itemDatabase.GetItem(gameMan.gameData.inventoryData.items[slot]);
        comboItem = null;

        if (item == inventoryMan.activeItem.secondItem)
        {
            comboItem = item.comboResult;
            gameMan.gameData.inventoryData.items[slot] = comboItem.itemName;
            RemoveActiveItem();
            return true;
        }

        ReturnActiveItem();
        return false;
    }

    public void SelectItem(int slot)
    {
        if (!gameMan.gameData.inventoryData.TryGetItem(slot, out var item))
            return;

        //if (item.Value == null)
        //{
        //    inventoryData = gameMan.gameData.inventoryData;
        //    inventoryData.TryGetItem(slot, out item);
        //}

        print($"Sellecting slot {item.Key} with item {item.Value}");

        inventoryMan.SetActiveItem(item.Value);
        gameMan.gameData.inventoryData.items[slot] = null;
        gameMan.SwitchGameState(GameManager.GameState.ItemHandling);

        AudioManager audioMan = AudioManager.Instance;
        itemsAudio.clip = audioMan.itemPickups[Random.Range(0, audioMan.itemPickups.Count)];
        itemsAudio.Play();
    }

    public void ReturnActiveItem()
    {
        if (inventoryMan.activeItem == null) return;

        TryAddItem(inventoryMan.activeItem.itemName);
        RemoveActiveItem();
    }

    public void RemoveActiveItem()
    {
        if (inventoryMan.activeItem == null) return;

        inventoryMan.SetActiveItem(null);

        AudioManager audioMan = AudioManager.Instance;
        itemsAudio.clip = audioMan.itemPickups[Random.Range(0, audioMan.itemPickups.Count)];
        itemsAudio.Play();
    }

    public bool CheckItemInInventory(string itemName)
    {
        if (gameMan.gameData.inventoryData == null)
            return false;

        foreach (var item_ in gameMan.gameData.inventoryData.items)
        {
            if (item_.Value == itemName)
                return true;
        }

        return false;
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
