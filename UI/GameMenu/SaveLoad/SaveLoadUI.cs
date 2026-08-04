using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] List<SaveLoadSlot> slots = new List<SaveLoadSlot>();
    [SerializeField] Sprite emptySprite;

    [Header("Buttons")]
    [SerializeField] Button saveBtn;
    [SerializeField] Button loadBtn;
    [SerializeField] Button overwriteBtn;
    [SerializeField] Button deleteBtn;

    [Header("Button Effects")]
    [SerializeField] UIElementEffects saveBtnEffects;
    [SerializeField] UIElementEffects loadBtnEffects;
    [SerializeField] UIElementEffects overwriteBtnEffects;
    [SerializeField] UIElementEffects deleteBtnEffects;

    [Header("Save file name")]
    [SerializeField] TMP_InputField inputField;

    SaveLoadManager saveMan;

    private void Start()
    {
        saveMan = SaveLoadManager.Instance;
    }

    public void SaveData()
    {
        SaveLoadManager.Instance.SaveGameData(inputField.text);
        ResetView();
    }

    public void DeleteData()
    {
        if (saveMan.sellectedSaveSlot == null) return;

        SaveLoadSystem.DeleteGameData(saveMan.sellectedSaveSlot.gameData, null);
        ResetView();
    }

    public void FillSlots()
    {
        List<GameData> gameDataList = SaveLoadSystem.GetAllGameData();

        foreach (var slot in slots)
        {
            if (slot == null) continue;

            GameData data = gameDataList.FirstOrDefault(item => item.index == slot.index);

            if (data == null || data == default)
            {
                slot.SetSlot(null, emptySprite);
                continue;
            }

            slot.SetSlot(data, SaveLoadSystem.LoadScreenshot($"GameData{slot.index}"));
        }
    }

    public void SellectSlot(int slotIndex)
    {
        SaveLoadSlot sellectedSlot = slots.FirstOrDefault(slot => slot.index == slotIndex);
        saveMan.sellectedSaveSlot = sellectedSlot;

        if (sellectedSlot.gameData == null)
        {
            SetSaveButton(true);
            SetLoadButton(false);
            SetOverwriteButton(false);
            SetDeleteButton(false);
        }

        else
        {
            SetSaveButton(false);
            SetLoadButton(true);
            SetOverwriteButton(true);
            SetDeleteButton(true);
        }

        foreach (var slot in slots) 
        {
            UIElementEffects effects = slot.GetComponent<UIElementEffects>();

            if (slot == sellectedSlot)
            {
                effects.OriginImageAlpha();
            }
            else
            {
                effects.ImageAlphaShade();
            }
        }
    }

    private void OnEnable()
    {
        ResetView();
    }

    private void ResetView()
    {
        foreach (var slot in slots)
        {
            slot.GetComponent<UIElementEffects>().SwitchToOriginImageAlpha();
        }

        FillSlots();

        saveMan.sellectedSaveSlot = null;

        SetSaveButton(false);
        SetLoadButton(false);
        SetOverwriteButton(false);
        SetDeleteButton(false);
    }

    private void SetButtonState(Button button, UIElementEffects effects, bool enabled)
    {
        button.interactable = enabled;

        if (enabled)
        {
            effects.OriginImageAlpha();
            effects.OriginTextAlpha();
        }
        else
        {
            effects.ImageAlphaShade();
            effects.TextAlphaShade();
        }
    }

    public void SetSaveButton(bool enabled)
    {
        SetButtonState(saveBtn, saveBtnEffects, enabled);
    }

    public void SetLoadButton(bool enabled)
    {
        SetButtonState(loadBtn, loadBtnEffects, enabled);
    }

    public void SetOverwriteButton(bool enabled)
    {
        SetButtonState(overwriteBtn, overwriteBtnEffects, enabled);
    }

    public void SetDeleteButton(bool enabled)
    {
        SetButtonState(deleteBtn, deleteBtnEffects, enabled);
    }
}
