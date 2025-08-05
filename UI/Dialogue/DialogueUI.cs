using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] RectTransform dialoguePanel;
    [SerializeField] RectTransform dialogueContent;
    [SerializeField] RectTransform textPref;
    [SerializeField] ScrollRect scrollRect;
    [Space]
    //[SerializeField] RectTransform choicesPanel;
    [SerializeField] RectTransform choicePref;

    List<RectTransform> content = new List<RectTransform>();
    List<RectTransform> choices = new List<RectTransform>();

    DialogueManager dialogueMan;

    private void Start()
    {
        dialogueMan = DialogueManager.Instance;
        dialoguePanel.gameObject.SetActive(false);
    }

    public void NewDialogue()
    {
        foreach (var item in content)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in choices)
        {
            Destroy(item.gameObject);
        }

        content.Clear();
        choices.Clear();

        dialoguePanel.gameObject.SetActive(true);
    }

    public void PrintDialogueText(string text, string choice)
    {
        string charName = string.Empty;

        foreach (var item in choices)
        {
            Destroy(item.gameObject);
        }

        choices.Clear();

        if (dialogueMan.activeCharacter != null)
            charName = dialogueMan.activeCharacter.GetName();

        if (choice != string.Empty)
        {
            var choiceText = Instantiate(textPref, dialogueContent);
            choiceText.GetComponent<DialogueTextUI>().SetText(choice, string.Empty);

            content.Add(choiceText);
        }

        var dialogueText = Instantiate(textPref, dialogueContent);
        dialogueText.GetComponent<DialogueTextUI>().SetText(text, name);

        content.Add(dialogueText);

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void PrintChoices(List<Choice> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(choices[i].text, i);
            this.choices.Add(choice);
        }

        if (choices.Count == 0)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice("Continue", 0);
            this.choices.Add(choice);
        }

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    public void ExitDialogue()
    {
        dialoguePanel.gameObject.SetActive(false);
    }
}
