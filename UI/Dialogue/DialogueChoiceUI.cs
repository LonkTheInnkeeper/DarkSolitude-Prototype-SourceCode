using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoiceUI : MonoBehaviour
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
}
