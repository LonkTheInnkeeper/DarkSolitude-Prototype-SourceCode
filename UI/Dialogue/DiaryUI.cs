using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiaryUI : MonoBehaviour
{
    [SerializeField] RectTransform diaryPanel;
    [SerializeField] RectTransform diaryScrollMask;
    [SerializeField] RectTransform diaryContent;
    [SerializeField] RectTransform textPref;
    [SerializeField] RectTransform choicePref;
    [SerializeField] ScrollRect scrollRect;

    [Space]
    [SerializeField] RectTransform slideLine;
    [SerializeField] float slideSpeed;
    [SerializeField] TextMeshProUGUI runningText;

    [Space]
    [SerializeField] Animator animator;

    [Header("Audio")]
    [SerializeField] AudioClip dialogueOpen;
    [SerializeField] AudioClip dialogueClose;

    List<RectTransform> content = new List<RectTransform>();

    StoryManager storyMan;

    private void Start()
    {
        storyMan = StoryManager.Instance;
        diaryPanel.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        SlideLine();
        RunningText();
    }

    public void OpenDiary()
    {
        for (int i = diaryContent.childCount - 1; i >= 0; i--)
        {
            Destroy(diaryContent.GetChild(i).gameObject);
        }

        content.Clear();

        diaryPanel.gameObject.SetActive(true);
        animator.SetTrigger("Open");

        AudioManager.Instance.PlayUI(dialogueOpen);
    }

    public void PrintDiary(string text, string choice, StoryTagData tags)
    {
        string charName = string.Empty;

        foreach (var item in content)
        {
            Destroy(item.gameObject);
        }

        content.Clear();

        //Add next story text
        var dialogueText = Instantiate(textPref, diaryContent);
        dialogueText.GetComponent<DialogueTextUI>().SetText(text, string.Empty);

        content.Add(dialogueText);
    }

    public void PrintChoices(List<Choice> choices)
    {
        for (int i = 0; i < choices.Count; i++)
        {
            var choice = Instantiate(choicePref, diaryContent);
            choice.GetComponent<DialogueChoiceUI>().SetChoice(choices[i].text, i);
            content.Add(choice);
        }
    }

    public void CloseDiary()
    {
        if (storyMan.exitAnimation != null)
        {
            storyMan.exitAnimation.TriggerAnimation("Close");
            storyMan.exitAnimation = null;
        }

        animator.SetTrigger("Close");

        GameManager.Instance.SetGameState(GameManager.GameState.Navigation);
        AudioManager.Instance.PlayUI(dialogueClose);

        GameEvents.OnDialogueEnd?.Invoke();
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

    private void RunningText()
    {
        if (!runningText.gameObject.activeInHierarchy) return;

        RectTransform rect = runningText.rectTransform;

        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + slideSpeed);

        if (rect.anchoredPosition.y >= 400)
        {
            rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, -400);
        }
    }

    public void SetRunningText(string text)
    {
        runningText.text = text;
    }
}
