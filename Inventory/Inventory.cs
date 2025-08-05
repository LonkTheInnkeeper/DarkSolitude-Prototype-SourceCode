using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] bool debugMessage = true;

    [SerializeField] TextMeshProUGUI activeItem;

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
    }

    private void Update()
    {
        if (inventoryMan.activeItem != null)
            activeItem.text = "Active item: " + inventoryMan.activeItem.itemName;
        else
            activeItem.text = "No active item";

        if (gameMan.gameState == GameManager.GameState.ItemHandling && Input.GetMouseButtonDown(1))
        {
            gameMan.SwitchGameState(GameManager.GameState.Navigation);
            AddItem(inventoryMan.activeItem);
            inventoryMan.activeItem = null;
        }
    }

    public void AddItem(ItemScriptable item)
    {
        if (item == null)
        {
            DebugMessage("Item " + item.name + " does not exist.");
            return;
        }

        inventoryMan.inventoryData.items.Add(item.itemName);
        uiMan.inventoryUI.AddItem(item);

        DebugMessage("Adding item: " + item.name);
    }

    public void AddItem(string itemName)
    {
        ItemScriptable item = database.GetItem(itemName);

        if (item == null)
        {
            DebugMessage("Item " + itemName + " does not exist.");
            return;
        }

        inventoryMan.inventoryData.items.Add(item.itemName);
        uiMan.inventoryUI.AddItem(item);

        DebugMessage("Adding item: " + item.name);
    }

    public void RemoveItem(ItemScriptable item)
    {
        inventoryMan.inventoryData.items.Remove(item.itemName);
    }

    public void ReturnItem()
    {
        AddItem(inventoryMan.activeItem);
        inventoryMan.activeItem = null;
    }

    public void CombineItems(ItemScriptable item)
    {

    }

    public void SellectItem(ItemScriptable item)
    {
        print("Sellecting item: " + item.itemName);

        inventoryMan.activeItem = item;
        inventoryMan.inventory.RemoveItem(item);
        gameMan.SwitchGameState(GameManager.GameState.ItemHandling);

        RemoveItem(item);
    }

    public bool CheckItemInInventory(string itemName)
    {
        return inventoryMan.inventoryData.CheckItem(itemName);
    }

    void DebugMessage(string message)
    {
        if (debugMessage)
            Debug.Log(message);
    }
}
