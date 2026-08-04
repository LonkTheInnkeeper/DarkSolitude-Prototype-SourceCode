using UnityEngine;

public class SettingsUI : MonoBehaviour
{
    GameManager gameMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
    }

    public void NextTextLang()
    {
        gameMan.settingsData.NextTextLang();
    }

    public void PreviousTextLang() 
    {
        gameMan.settingsData.PreviousTextLang();
    }

    public void NextVoiceLang()
    {
        gameMan.settingsData.NextVoiceLang();
    }

    public void PreviousVoiceLang()
    {
        gameMan.settingsData.PreviousVoiceLang();
    }
}
