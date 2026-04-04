using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public InventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    public DiaryUI diaryUI;
    public InfoTextUI infoTextUI;
    public LocationShade locationShade;
    public RectTransform mousePoint;

    [Space]
    public GameObject closeupUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        closeupUI.SetActive(GameManager.Instance.gameState == GameManager.GameState.Closeup);
    }
}
