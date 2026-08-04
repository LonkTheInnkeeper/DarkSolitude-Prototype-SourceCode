using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] RectTransform dialoguePanel;
    [SerializeField] RectTransform dialogueContent;
    [SerializeField] RectTransform textPref;
    [SerializeField] RectTransform playerTextPref;
    [SerializeField] RectTransform choicePref;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] Image portrait;
    [SerializeField] Sprite defaultPortrait;
    [SerializeField] float scrollSpeed;

    [Space]
    [SerializeField] RectTransform slideLine;
    [SerializeField] float slideSpeed;

    [Space]
    [SerializeField] Animator animator;

    [Header("Audio")]
    [SerializeField] AudioClip dialogueOpen;
    [SerializeField] AudioClip dialogueClose;

    List<RectTransform> content = new List<RectTransform>();
    List<RectTransform> choices = new List<RectTransform>();

    StoryManager storyMan;

    private void Start()
    {
        storyMan = StoryManager.Instance;
        dialoguePanel.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        SlideLine();
    }

    public void OpenDialogue()
    {
        for (int i = dialogueContent.childCount - 1; i >= 0; i--)
        {
            Destroy(dialogueContent.GetChild(i).gameObject);
        }

        content.Clear();
        choices.Clear();

        dialoguePanel.gameObject.SetActive(true);
        animator.SetTrigger("Open");

        AudioManager.Instance.PlayUI(dialogueOpen);
    }

    public void PrintStoryBlock(StoryBlock block)
    {
        PrintDialogueText(block.text, block.lastChoice, block.tagData);
        PrintChoices(block.choiceList);
    }

    public void PrintDialogueText(string text, string choice, StoryTagData tags)
    {
        CharacterScriptable activeCharacter = null;

        foreach (var item in choices)
        {
            Destroy(item.gameObject);
        }

        choices.Clear();

        if (storyMan.activeCharacter != null)
        {
            activeCharacter = storyMan.activeCharacter;
            portrait.sprite = activeCharacter.portrait;
        }
        else
        {
            portrait.sprite = defaultPortrait;
        }

        // Add text of the choice to the content
        if (choice != string.Empty)
        {
            var choiceText = Instantiate(playerTextPref, dialogueContent);
            choiceText.GetComponent<DialogueTextUI>().SetText(choice, string.Empty);

            content.Add(choiceText);
        }

        // Add next story text
        var dialogueText = Instantiate(textPref, dialogueContent);
        dialogueText.GetComponent<DialogueTextUI>().SetText(text, activeCharacter != null ? activeCharacter.GetName() : string.Empty);

        content.Add(dialogueText);
    }

    public void PrintChoices(List<Choice> choices)
    {
        print("Choices: " + choices.Count);

        for (int i = 0; i < choices.Count; i++)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(choices[i].text, i);
            this.choices.Add(choice);
        }

        if (choices.Count == 0)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(UIManager.Instance.textData.uiTexts["dialogue_continue"], 0);
            this.choices.Add(choice);
        }

        StartCoroutine(ScrollContent());
    }

    public void PrintChoices(List<string> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(choices[i], i);
            this.choices.Add(choice);
        }

        if (choices.Count == 0)
        {
            var choice = Instantiate(choicePref, dialogueContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(UIManager.Instance.textData.uiTexts["dialogue_continue"], 0);
            this.choices.Add(choice);
        }

        StartCoroutine(ScrollContent());
    }

    public void CloseDialogue()
    {
        animator.SetTrigger("Close");
 
        AudioManager.Instance.PlayUI(dialogueClose);
    }

    IEnumerator ScrollContent()
    {
        yield return new WaitForEndOfFrame();

        Canvas.ForceUpdateCanvases();

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scrollSpeed;

            scrollRect.verticalNormalizedPosition =
                Mathf.Lerp(scrollRect.verticalNormalizedPosition, 0f, t);

            yield return null;
        }

        scrollRect.verticalNormalizedPosition = 0f;
    }

    private void SlideLine()
    {
        if (!slideLine.gameObject.activeInHierarchy) return;

        slideLine.anchoredPosition = new Vector2(slideLine.anchoredPosition.x, slideLine.anchoredPosition.y + slideSpeed);

        if (slideLine.anchoredPosition.y >= 500)
        {
            slideLine.anchoredPosition = new Vector2(slideLine.anchoredPosition.x, -500);
        }
    }
}
