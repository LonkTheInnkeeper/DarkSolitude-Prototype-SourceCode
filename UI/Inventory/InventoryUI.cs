using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUITrigger
{
    public List<InventoryItemUI> items;
    [SerializeField] RectTransform inventoryPanel;
    [SerializeField] Animator animator;
    [SerializeField] float collapseDistance;
    [SerializeField] GameObject activeItemUI;
    [SerializeField] TextMeshProUGUI activeItemName;
    [SerializeField] Image activeItemRenderer;

    GameManager gameMan;
    InventoryManager inventoryMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        float distance = Vector2.Distance(inventoryPanel.localPosition, UIManager.Instance.mousePoint.localPosition);

        if (gameMan.gameState == GameManager.GameState.Inventory && (distance > collapseDistance || InputManager.Instance.RightClick()))
            ToggleInventory();

        if (inventoryMan.activeItem != null)
        {
            activeItemUI.SetActive(true);
            activeItemName.text = inventoryMan.activeItem.itemName;
            activeItemRenderer.sprite = inventoryMan.activeItem.inventoryIcon;
        }
        else
        {
            activeItemUI.SetActive(false);
        }
    }

    public void ToggleInventory()
    {
        if (gameMan.gameState == GameManager.GameState.Navigation)
        {
            gameMan.SwitchGameState(GameManager.GameState.Inventory);
            inventoryPanel.gameObject.SetActive(true);
            animator.SetTrigger("Open");
        }
        else if (gameMan.gameState == GameManager.GameState.Inventory)
        {
            gameMan.SwitchGameState(GameManager.GameState.Navigation);
            animator.SetTrigger("Close");
        }
    }

    public void CloseInventory()
    {
        if (inventoryPanel.gameObject.activeInHierarchy)
            inventoryPanel.gameObject.SetActive(false);

        if (gameMan.gameState == GameManager.GameState.Inventory)
        {
            gameMan.SwitchGameState(GameManager.GameState.Navigation);
        }
    }

    public void AddItem(ItemScriptable item)
    {
        foreach (var itemUI in items)
        {
            if (itemUI.IsEmpty())
            {
                itemUI.SetItem(item);
                break;
            }
        }
    }

    public List<ItemScriptable> GetAllItems()
    {
        List<ItemScriptable> allItems = new List<ItemScriptable>();
        foreach (var item in items)
        {
            if (item.GetItem() != null)
                allItems.Add(item.GetItem());
        }

        return allItems;
    }

    public void Trigger(bool trigger, string name)
    {
        //if (!trigger && gameMan.gameState == GameManager.GameState.Inventory)
        //{
        //    gameMan.SwitchGameState(GameManager.GameState.Navigation);
        //    animator.SetTrigger("Close");
        //}
    }
}
