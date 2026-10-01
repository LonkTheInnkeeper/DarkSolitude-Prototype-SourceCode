using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public InventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    public DiaryUI diaryUI;
    public InfoTextUI infoTextUI;
    public CommentUI commentUI;
    public ScreenShade screenShade;
    public RectTransform mousePoint;
    public HintsHandler hintsHandler;
    public UITextData textData;
    public SaveLoadUI saveLoadUI;

    [Space]
    public GameObject closeupUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //SetTextData();
        textData = SaveLoadSystem.LoadUITextData(GameManager.Instance.settingsData.textLang);
        GameEvents.OnTextLanguageChange?.Invoke();
    }

    private void Update()
    {
        if (closeupUI != null)
            closeupUI.SetActive(GameManager.Instance.GetGameState() == GameManager.GameState.Closeup);
    }

    public void SetTextData()
    {
        textData = SaveLoadSystem.LoadUITextData(GameManager.Instance.settingsData.textLang);
        GameEvents.OnTextLanguageChange?.Invoke();
    }
}
