using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceUI : MonoBehaviour, IUITrigger
{
    [SerializeField] TextMeshProUGUI choiceText;
    Button button;
    int choiceIndex;

    public void SetChoice(string choiceText, int index)
    {
        this.choiceText.fontSize = GameManager.Instance.settingsData.fontSize;

        this.choiceText.text = (index + 1).ToString() + ". " + choiceText;
        this.choiceIndex = index;

        button = GetComponent<Button>();
        button.onClick.AddListener(() => MakeChoice());
    }

    public void MakeChoice()
    {
        DialogueManager.Instance.dialogue.MakeChoice(choiceIndex);
    }

    public void Trigger(bool trigger, string name)
    {
        if (trigger)
        {
            GetComponent<Animator>().SetTrigger("Sellect");
        }
        else
        {
            GetComponent<Animator>().SetTrigger("Desellect");
        }
    }
}
