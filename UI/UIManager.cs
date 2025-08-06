using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public InventoryUI inventoryUI;
    public DialogueUI dialogueUI;
    public DiaryUI diaryUI;

    private void Awake()
    {
        Instance = this;
    }
}
