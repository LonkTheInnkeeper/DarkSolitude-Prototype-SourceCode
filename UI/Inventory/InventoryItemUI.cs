using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    ItemScriptable item;
    Image icon;
    [SerializeField] Sprite defaultIcon;

    InventoryManager inventoryMan;
    GameManager gameMan;

    private void Start()
    {
        inventoryMan = InventoryManager.Instance;
        gameMan = GameManager.Instance;
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
        switch (gameMan.gameState)
        {
            case GameManager.GameState.Inventory:
                {
                    if (item == null) return;

                    inventoryMan.inventory.SelectItem(item);
                    SetItem(null);

                    break;
                }

            case GameManager.GameState.ItemHandling:
                {
                    if (item != null)
                    {
                        if (inventoryMan.activeItem != null)
                        {
                            ItemScriptable comboItem = inventoryMan.inventory.CombineItems(item);
                            if (comboItem != null)
                            {
                                inventoryMan.SetActiveItem(null);
                                SetItem(comboItem);
                                gameMan.SwitchGameState(GameManager.GameState.Inventory);
                            }
                        }

                        return;
                    }

                    SetItem(inventoryMan.activeItem);
                    inventoryMan.inventory.ReturnItem();
                    gameMan.SwitchGameState(GameManager.GameState.Inventory);

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
