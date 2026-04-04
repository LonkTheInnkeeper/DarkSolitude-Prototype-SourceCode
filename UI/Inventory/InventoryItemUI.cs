using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IUITrigger
{
    ItemScriptable item;
    public int slotIndex;
    [SerializeField] Image icon;
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

                    inventoryMan.inventory.SelectItem(slotIndex);
                    SetItem(null);

                    break;
                }

            case GameManager.GameState.ItemHandling:
                {
                    // Combine items
                    if (item != null && inventoryMan.activeItem != null)
                    {
                        if (inventoryMan.inventory.TryCombineItems(slotIndex, out var comboItem))
                        {
                            SetItem(comboItem);
                            GameManager.Instance.gameData.inventoryData.items[slotIndex] = comboItem.itemName;
                            gameMan.SwitchGameState(GameManager.GameState.Inventory);
                        }
                        return;
                    }

                    // Place item
                    SetItem(inventoryMan.activeItem);
                    GameManager.Instance.gameData.inventoryData.items[slotIndex] = inventoryMan.activeItem.itemName;
                    inventoryMan.inventory.RemoveActiveItem();
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

    public ItemScriptable GetItem()
    {
        return item;
    }

    public void UIAnimationTrigger(bool trigger, string name)
    {
        if (trigger)
        {
            GetComponent<Animator>().SetTrigger("Sellect");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Desellect");
        }
    }
}
