using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] RectTransform diaryPanel;
    [SerializeField] RectTransform diaryContent;
    [SerializeField] RectTransform textPref;
    [SerializeField] RectTransform choicePref;

    List<RectTransform> content = new List<RectTransform>();

    DialogueManager dialogueMan;

    private void Start()
    {
        dialogueMan = DialogueManager.Instance;
    }

    public void NewDiary()
    {
        foreach (var item in content)
        {
            Destroy(item.gameObject);
        }

        content.Clear();

        diaryPanel.gameObject.SetActive(true);
    }

    public void PrintDiary(Story currentDiary)
    {

    }
}
