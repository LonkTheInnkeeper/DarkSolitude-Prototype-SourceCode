using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Inventory inventory;
    public ItemDatabase itemDatabase;
    public ItemScriptable activeItem;
    public int inventorySize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
    }

    public void SetActiveItem(string itemName)
    {
        if (itemName != null)
        {
            activeItem = itemDatabase.GetItem(itemName);
            GameManager.Instance.mouseControl.SetItemCursor(activeItem.cursorIcon);
        }
        else
        {
            activeItem = null;
            GameManager.Instance.mouseControl.SetDefaultCursor();
        }
    }
}
