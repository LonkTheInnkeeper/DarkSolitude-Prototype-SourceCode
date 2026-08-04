using TMPro;
using UnityEngine;

public class VoiceLanguageTextUI : MonoBehaviour
{
    TextMeshProUGUI uiText;
    string key = "localisation";
    UITextData textData;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        PrintText();
    }

    void PrintText()
    {
        textData = SaveLoadSystem.LoadUITextData(GameManager.Instance.settingsData.voiceLang);

        //if (uiText == null)
        //    uiText = GetComponent<TextMeshProUGUI>();

        uiText.text = textData.uiTexts[key];
    }

    private void OnEnable()
    {
        GameEvents.OnTextLanguageChange += PrintText;
    }
}
