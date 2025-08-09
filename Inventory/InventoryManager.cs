using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Inventory inventory;
    public ItemDatabase itemDatabase;

    public InventoryData inventoryData;

    public ItemScriptable activeItem;

    private void Awake()
    {
        Instance = this;
    }

    public void SetActiveItem(ItemScriptable item)
    {
        if (item != null)
        {
            GameManager.Instance.mouseControl.SetItemCursor(item.cursorIcon);
        }
        else
        {
            GameManager.Instance.mouseControl.SetDefaultCursor();
        }

        activeItem = item;
    }
}
