using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    ItemScriptable item;
    public int slotIndex;
    [SerializeField] Image icon;
    [SerializeField] Sprite defaultIcon;
    [SerializeField] ItemDescriptionUI itemDescription;

    [SerializeField] string itemSellect;
    [SerializeField] string itemCombo;

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
        if (gameMan.GetGameState() == GameManager.GameState.Navigation ||
            gameMan.GetGameState() == GameManager.GameState.Closeup)
        {
            if (item == null) return;

            inventoryMan.inventory.SelectItem(slotIndex);
            SetItem(null);
            AudioManager.Instance.ui.Play(itemSellect);
        }

        else if (gameMan.GetGameState() == GameManager.GameState.ItemHandling)
        {
            // Combine items
            if (item != null && inventoryMan.activeItem != null)
            {
                if (inventoryMan.inventory.TryCombineItems(slotIndex, out var comboItem))
                {
                    SetItem(comboItem);
                    AudioManager.Instance.ui.Play(itemCombo);
                    GameManager.Instance.gameData.inventoryData.items[slotIndex] = comboItem.itemName;
                }

                UIManager.Instance.inventoryUI.FillInventorySlots();
                //gameMan.SetGameState(GameManager.GameState.Navigation);
                return;
            }

            // Place item
            SetItem(inventoryMan.activeItem);
            AudioManager.Instance.ui.Play(itemSellect);
            GameManager.Instance.gameData.inventoryData.items[slotIndex] = inventoryMan.activeItem.itemName;
            inventoryMan.inventory.DesellectActiveItem();
        }

        else
        {
            print("Unexpected inventory action");
        }
    }

    public void ToggleDescription()
    {
        itemDescription.ToggleDescription(item);
    }

    public ItemScriptable GetItem()
    {
        return item;
    }
}
