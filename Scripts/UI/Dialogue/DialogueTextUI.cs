using TMPro;
using UnityEngine;

public class DialogueTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI charName;

    public void SetText(string text, string name)
    {
        dialogueText.text = text;
        charName.text = name;
    }
}
