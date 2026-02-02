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

    private void Awake()
    {
        Instance = this;
    }
}
