using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceUI : MonoBehaviour, IUITrigger
{
    [SerializeField] TextMeshProUGUI choiceText;
    Button button;
    int choiceIndex;

    [Space]
    [SerializeField] string dialogueSellect;
    [SerializeField] string dialogueHover;

    public void SetChoice(string choiceText, int index)
    {
        //this.choiceText.fontSize = GameManager.Instance.settingsData.fontSize;

        this.choiceText.text = (index + 1).ToString() + ". " + choiceText;
        this.choiceIndex = index;

        button = GetComponent<Button>();
        button.onClick.AddListener(() => MakeChoice());
    }

    public void MakeChoice()
    {
        AudioManager.Instance.ui.Play(dialogueSellect);
        StoryManager.Instance.MakeStoryChoice(choiceIndex);
    }

    public void CursorOnUI(bool trigger)
    {
        if (trigger)
        {
            AudioManager.Instance.ui.Play(dialogueHover);
            GetComponent<Animator>().SetTrigger("Sellect");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Desellect");
        }
    }
}
