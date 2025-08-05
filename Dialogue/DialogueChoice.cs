using TMPro;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    int choiceIndex = 0;
    [SerializeField] TextMeshProUGUI choiceTextUI;
    [SerializeField] GameObject background;

    public void Init(int choiceIndex, string choiceText)
    {
        this.choiceIndex = choiceIndex;
        choiceTextUI.text = (choiceIndex + 1) + ". " + choiceText;
    }

    public void ToggleBackground(bool toggle)
    {
        background.SetActive(toggle);
    }

    public void MakeChoice()
    {
        DialogueManager.Instance.dialogue.MakeChoice(choiceIndex);
    }
}
