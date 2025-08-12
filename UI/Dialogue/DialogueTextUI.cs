using TMPro;
using UnityEngine;

public class DialogueTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI charName;

    public void SetText(string text, string name)
    {
        dialogueText.fontSize = GameManager.Instance.settingsData.fontSize;
        charName.fontSize = GameManager.Instance.settingsData.fontSize;

        dialogueText.text = text;
        charName.text = name;
    }
}
