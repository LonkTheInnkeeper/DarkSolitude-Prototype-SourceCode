using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndCredits : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI header;
    [SerializeField] TextMeshProUGUI left;
    [SerializeField] TextMeshProUGUI right;
    [SerializeField] TextMeshProUGUI skipButtonText;
    [Space]
    [SerializeField] int rollSpeed;
    [SerializeField] ScreenShade shade;

    RectTransform rect;
    CreditsData creditsData;
    UITextData uITextData;

    void Start()
    {
        SettingsData settings = SaveLoadSystem.LoadSettingsData();

        rect = GetComponent<RectTransform>();
        creditsData = SaveLoadSystem.LoadEndCreditsData(settings.textLang);
        uITextData = SaveLoadSystem.LoadUITextData(settings.textLang);

        skipButtonText.text = uITextData.uiTexts["endCreditsButton"];
        PrintCredits();
    }

    private void Update()
    {
        rect.anchoredPosition += Vector2.up * rollSpeed * Time.deltaTime; 
    }

    void PrintCredits()
    {
        header.text = creditsData.header;
        left.text = string.Empty;
        right.text = string.Empty;

        foreach (var section in creditsData.sections)
        {
            left.text += section.title;

            foreach (var name in section.names)
            {
                right.text += name + "\n";
                left.text += "\n";
            }

            right.text += "\n";
            left.text += "\n";
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }
}
