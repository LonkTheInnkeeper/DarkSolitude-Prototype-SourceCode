using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTextUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] TextMeshProUGUI charName;

    public void SetText(string text, string? name)
    {
        //dialogueText.fontSize = GameManager.Instance.settingsData.fontSize;
        //charName.fontSize = GameManager.Instance.settingsData.fontSize;

        dialogueText.text = text;

        //StartCoroutine(Print(text));

        charName.text = name;
    }

    IEnumerator Print(string text)
    {
        foreach (char char_ in text)
        {
            dialogueText.text += char_;
            yield return new WaitForSeconds(0.00000001f);
        }
    }
}
