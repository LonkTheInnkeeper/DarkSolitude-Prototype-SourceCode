using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    ItemScriptable item;
    Image icon;
    [SerializeField] Sprite defaultIcon;

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
            icon.sprite = defaultIcon;
            return;
        }

        this.item = item;
        icon.sprite = item.inventoryIcon;
    }

    public bool IsEmpty()
    {
        return item == null;
    }

    public void SlotClick()
    {
        switch (GameManager.Instance.gameState)
        {
            case GameManager.GameState.Inventory:
                {
                    if (item == null) return;

                    if (inventoryMan.activeItem != null)
                    {
                        inventoryMan.inventory.CombineItems(item);
                    }
                    else
                    {
                        inventoryMan.inventory.SelectItem(item);
                    }

                    SetItem(null);

                    break;
                }

            case GameManager.GameState.ItemHandling:
                {
                    if (item != null) return;

                    SetItem(inventoryMan.activeItem);
                    inventoryMan.inventory.ReturnItem();
                    GameManager.Instance.SwitchGameState(GameManager.GameState.Inventory);

                    break;
                }

            default:
                {
                    print("Unexpected inventory action");
                    break;
                }
        }
    }
}
