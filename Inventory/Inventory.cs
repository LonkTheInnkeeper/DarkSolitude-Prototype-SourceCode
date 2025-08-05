using TMPro;
using UnityEngine;
using static UnityEditor.Progress;

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
    }

    public void AddItem(ItemScriptable item)
    {
        if (item == null)
        {
            DebugMessage("Item does not exist.");
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

    public void CombineItems(ItemScriptable item)
    {

    }

    public void SelectItem(ItemScriptable item)
    {
        print("Sellecting item: " + item.itemName);

        inventoryMan.activeItem = item;
        RemoveItem(item);
        gameMan.SwitchGameState(GameManager.GameState.ItemHandling);

        RemoveItem(item);
    }

    public void DesellectItem()
    {
        AddItem(inventoryMan.activeItem);
        ReturnItem();
        uiMan.inventoryUI.CloseInventory();
        gameMan.SwitchGameState(GameManager.GameState.Navigation);
    }

    public void ReturnItem()
    {
        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventoryData.items.Add(inventoryMan.activeItem.itemName);
            inventoryMan.activeItem = null;
        }
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
