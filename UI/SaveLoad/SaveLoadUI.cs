using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] List<SaveLoadSlot> slots;
    [SerializeField] SaveLoadSlot activeSlot;
    [SerializeField] TextMeshProUGUI inputFieldText;

    [Space]
    [SerializeField] Button saveButton;
    [SerializeField] Button overWriteButton;
    [SerializeField] Button loadButton;
    [SerializeField] Button deleteButton;
    [SerializeField] Button closeButton;

    [Space]
    public Sprite defaultSprite;

    bool toggled = false;
    bool inputToggled = false;

    private void Start()
    {
        toggled = false;
        animator.gameObject.SetActive(false);
    }

    public void ToggleSaveLoad()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Navigation &&
            GameManager.Instance.gameState != GameManager.GameState.Menu)
            return;

        animator.gameObject.SetActive(true);

        if (toggled)
        {
            animator.SetTrigger("Close");
            toggled = false;
            GameManager.Instance.SwitchGameState(GameManager.GameState.Navigation);
        }
        else
        {
            animator.SetTrigger("Open");
            toggled = true;
            FillSlots();
            GameManager.Instance.SwitchGameState(GameManager.GameState.Menu);
        }
    }

    public void ToggleInputField()
    {
        if (inputToggled)
        {
            animator.SetTrigger("InputClose");
            inputToggled = false;
        }
        else
        {
            animator.SetTrigger("InputOpen");
            inputToggled = true;
        }
    }

    public void FillSlots()
    {
        List<GameData> gameDataList = SaveLoadSystem.GetAllGameData();

        print("Data list count " +  gameDataList.Count);

        foreach (var slot in slots)
        {
            slot.SetSlot(null, defaultSprite);
        }

        for (int i = 0; i < slots.Count; i++)
        {
            foreach (GameData gameData in gameDataList)
            {
                if (gameData.index == slots[i].index)
                {
                    Sprite sprite = SaveLoadSystem.LoadScreenshot("GameData" + gameData.index);

                    if (sprite == null)
                    {
                        sprite = defaultSprite;
                        Debug.LogWarning("Screenshot not found. Swithing to default");
                    }

                    slots[i].SetSlot(gameData, sprite);
                }
                else
                    continue;
            }
        }

        SetButtons();
    }

    public void SaveData()
    {
        print("Saving data to slot " + activeSlot.index);

        SaveLoadManager.Instance.SavePlayerData(activeSlot.index, inputFieldText.text);
        FillSlots();
    }

    public void LoadData()
    {
        //SaveLoadManager.Instance.LoadPlayerData(activeSlot.playerData);

        UIManager.Instance.locationShade.StartLoadig(activeSlot.gameData);
        ToggleSaveLoad();
    }

    public void DeleteData()
    {
        if (activeSlot.gameData == null) return;

        if (SaveLoadSystem.DeleteGameData(activeSlot.gameData, activeSlot.screenshot))
        {
            activeSlot.SetSlot(null, defaultSprite);
            activeSlot = null;
        }

        FillSlots();
    }

    public void SetActiveSlot(int index)
    {
        print("Looking for slot");

        foreach (var slot in slots)
        {
            if (slot.index == index)
            {
                activeSlot = slot;
                Debug.Log("Active Save slot: " + index);
                SetButtons();
                return;
            }
        }

        Debug.LogWarning("Save slot not found");
    }

    void SetButtons()
    {
        if (activeSlot == null)
        {
            saveButton.interactable = false;
            overWriteButton.interactable = false;
            loadButton.interactable = false;
            deleteButton.interactable = false;
        }
        else if (activeSlot != null && activeSlot.gameData == null)
        {
            saveButton.interactable = true;
            overWriteButton.interactable = false;
            loadButton.interactable = false;
            deleteButton.interactable = false;
        }
        else if (activeSlot != null && activeSlot.gameData != null)
        {
            saveButton.interactable = false;
            overWriteButton.interactable = true;
            loadButton.interactable = true;
            deleteButton.interactable = true;
        }
    }
}
