using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    TextMeshProUGUI uiText;
    public string key;

    void Start()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        //PrintText();
    }

    void PrintText()
    {
        if (uiText == null)
            uiText = GetComponent<TextMeshProUGUI>();

        if (UIManager.Instance == null) return;

        uiText.text = UIManager.Instance.textData.GetUiText(key);
    }

    private void OnEnable()
    {
        GameEvents.OnTextLanguageChange += PrintText;
        PrintText();
    }

    private void OnDisable()
    {
        GameEvents.OnTextLanguageChange -= PrintText;
    }

    public void SetKey(string key)
    {
        this.key = key;
        PrintText();
    }
}
