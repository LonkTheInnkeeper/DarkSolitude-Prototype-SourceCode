using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour, IUITrigger
{
    ItemScriptable item;
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

    public ItemScriptable GetItem()
    {
        return item;
    }

    public void Trigger(bool trigger, string name)
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
