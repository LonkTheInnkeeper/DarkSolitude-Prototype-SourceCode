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

    GameManager gameMan;
    InventoryManager inventoryMan;
    AudioManager audioMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
        inventoryPanel.gameObject.SetActive(false);
        audioMan = AudioManager.Instance;
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

        audioMan.PlayUI(audioMan.database.GetInventoryClip("InventoryNotification"));

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
        audioMan.PlayUI(audioMan.database.GetInventoryClip("InventoryUp"));
    }

    public void CloseInventory()
    {
        animator.SetTrigger("InventoryFrameClose");
        audioMan.PlayUI(audioMan.database.GetInventoryClip("InventoryDown"));
    }
}
