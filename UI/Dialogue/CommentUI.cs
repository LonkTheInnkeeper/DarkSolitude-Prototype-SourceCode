using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommentUI : MonoBehaviour
{
    [SerializeField] RectTransform commentPanel;
    [SerializeField] TextMeshProUGUI commentText;
    [SerializeField] Image portrait;
    [SerializeField] Sprite defaultPortrait;

    [Space]
    [SerializeField] Animator animator;

    StoryManager storyMan;

    private void Start()
    {
        storyMan = StoryManager.Instance;
        commentPanel.gameObject.SetActive(false);
    }

    public void OpenComment()
    {
        commentPanel.gameObject.SetActive(true);
        print("Openning comment");
        animator.SetTrigger("Open");
    }

    public void CloseComment()
    {
        animator.SetTrigger("Close");
    }

    public void PrintCommentText(string text, string choice, StoryTagData tags)
    {
        commentText.text = text;
        SwitchPortrait();
    }

    public void NextComment()
    {
        storyMan.MakeStoryChoice(0);
    }

    void SwitchPortrait()
    {
        if (storyMan.GetActiveCharacter() == null)
        {
            portrait.sprite = defaultPortrait;
            return;
        }

        portrait.sprite = storyMan.GetActiveCharacter().portrait;
    }

    public Story ProcessStory(Story story) => story;
}
