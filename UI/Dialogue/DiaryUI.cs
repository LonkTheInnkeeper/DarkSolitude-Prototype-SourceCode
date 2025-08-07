using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] RectTransform diaryPanel;
    [SerializeField] RectTransform diaryContent;
    [SerializeField] RectTransform textPref;
    [SerializeField] RectTransform choicePref;
    [SerializeField] ScrollRect scrollRect;

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

    public Story PrintDiary(Story currentDiary)
    {
        NewDiary();
        List<Choice> choices = new List<Choice>(currentDiary.currentChoices);

        string diaryText = currentDiary.currentText;

        while (choices.Count == 0)
        {
            diaryText += currentDiary.Continue() + "\n \n";
            choices = new List<Choice>(currentDiary.currentChoices);
        }

        var text = Instantiate(textPref, diaryContent);
        text.GetComponent<DialogueTextUI>().SetText(diaryText, null);
        content.Add(text);

        for (int i = 0; i < choices.Count; i++)
        {
            var choice = Instantiate(choicePref, diaryContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(choices[i].text, i);
            content.Add(choice);
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 1f;

        return currentDiary;
    }

    public void ExitDiary()
    {
        dialogueMan.dialogue.ExitDialogue();
        diaryPanel.gameObject.SetActive(false);
    }
}
