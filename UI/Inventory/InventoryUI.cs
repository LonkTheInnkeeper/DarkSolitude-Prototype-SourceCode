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
        // Inventory UI panel collapsing
        float distance = Vector2.Distance(inventoryPanel.localPosition, UIManager.Instance.mousePoint.localPosition);

        if (gameMan.gameState == GameManager.GameState.Inventory &&
           (distance > collapseDistance || InputManager.Instance.RightClick()))
            ToggleInventory();

        // Active item UI handling
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
        if (gameMan.gameState == GameManager.GameState.Navigation ||
            gameMan.gameState == GameManager.GameState.Closeup)
        {
            FillInventorySlots();

            gameMan.SwitchGameState(GameManager.GameState.Inventory);
            inventoryPanel.gameObject.SetActive(true);
            animator.SetTrigger("Open");
        }
        else if (gameMan.gameState == GameManager.GameState.Inventory ||
                 gameMan.gameState == GameManager.GameState.ItemHandling ||
                 gameMan.gameState == GameManager.GameState.Closeup)
        {
            if (!gameMan.closeupState)
                gameMan.SwitchGameState(GameManager.GameState.Navigation);
            else
                gameMan.SwitchGameState(GameManager.GameState.Closeup);

            animator.SetTrigger("Close");
        }
    }

    void FillInventorySlots()
    {
        InventoryData inventoryData = GameManager.Instance.gameData.inventoryData;

        foreach (var itemSlot in items)
        {
            if (inventoryData.TryGetItem(itemSlot.slotIndex, out var item))
            {
                itemSlot.SetItem(inventoryMan.itemDatabase.GetItem(item.Value));
            }
        }
    }

    public void UIAnimationTrigger(bool trigger, string name)
    {
        //if (!trigger && gameMan.gameState == GameManager.GameState.Inventory)
        //{
        //    gameMan.SwitchGameState(GameManager.GameState.Navigation);
        //    animator.SetTrigger("Close");
        //}
    }
}
