using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour, IUITrigger
{
    public List<InventoryItemUI> inventorySlotList;
    [SerializeField] RectTransform inventoryPanel;
    [SerializeField] Animator animator;
    [SerializeField] Animator notificationAnimator;
    [SerializeField] GameObject activeItemUI;
    [SerializeField] TextMeshProUGUI activeItemName;
    [SerializeField] Image activeItemRenderer;

    [Space]
    [SerializeField] Image notificationIcon;

    [Space]
    [SerializeField] string inventoryNofitication;
    [SerializeField] string inventoryUp;
    [SerializeField] string inventoryDown;

    GameManager gameMan;
    InventoryManager inventoryMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
        inventoryPanel.gameObject.SetActive(false);
    }

    public void FillInventorySlots()
    {
        InventoryData inventoryData = GameManager.Instance.gameData.inventoryData;

        foreach (var itemSlot in inventorySlotList)
        {
            if (inventoryData.TryGetItem(itemSlot.slotIndex, out var item))
            {
                itemSlot.SetItem(inventoryMan.itemDatabase.GetItem(item.Value));
            }
        }
    }

    public void ItemNotification(ItemScriptable item)
    {
        StartCoroutine(NotificationRoutine(item.inventoryIcon));
    }

    IEnumerator NotificationRoutine(Sprite icon)
    {
        notificationIcon.sprite = icon;
        notificationAnimator.SetTrigger("Open");

        AudioManager.Instance.ui.Play(inventoryNofitication);

        yield return new WaitForSeconds(2);

        notificationAnimator.SetTrigger("Close");
    }

    public void CursorOnUI(bool trigger)
    {
        throw new System.NotImplementedException();
    }

    public void OpenInventory()
    {
        animator.SetTrigger("InventoryFrameOpen");
        AudioManager.Instance.ui.Play(inventoryUp);
    }

    public void CloseInventory()
    {
        animator.SetTrigger("InventoryFrameClose");
        AudioManager.Instance.ui.Play(inventoryDown);
    }
}
