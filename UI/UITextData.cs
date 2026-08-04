using System.Collections.Generic;

public class UITextData
{
    public Dictionary<string, string> uiTexts;

    public string GetUiText(string key)
    {
        if (uiTexts[key] == null)
        {
            return $"#UI text {key} missing#";
        }

        else
        {
            return uiTexts[key];
        }
    }
}
