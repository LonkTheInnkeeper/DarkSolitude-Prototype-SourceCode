using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    ItemScriptable item;
    Image icon;

    InventoryManager inventoryMan;

    private void Start()
    {
        inventoryMan = InventoryManager.Instance;
    }

    public void SetItem(ItemScriptable item)
    {
        icon = GetComponent<Image>();

        if (item == null)
        {
            this.item = null;
            icon.sprite = null;
            return;
        }

        this.item = item;
        icon.sprite = item.inventoryIcon;
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public void SellectItem()
    {
        if (item == null || GameManager.Instance.gameState != GameManager.GameState.Inventory)
            return;

        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventory.CombineItems(item);
        }
        else
        {
            inventoryMan.inventory.SellectItem(item);
        }

        SetItem(null);
    }
}
