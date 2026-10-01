using System;
using UnityEngine.InputSystem;

[Serializable]
public class SettingsData
{
    public TextLang textLang;
    public VoiceLang voiceLang;
    public float music;
    public float effects;
    public float voice;

    public enum VoiceLang
    {
        en,
        cz
    }

    public enum TextLang
    {
        en,
        cz
    }

    public void NextTextLang()
    {
        int count = System.Enum.GetValues(typeof(TextLang)).Length;
        textLang = (TextLang)(((int)textLang + 1) % count);

        UIManager.Instance.SetTextData();
    }

    public void PreviousTextLang()
    {
        int count = System.Enum.GetValues(typeof(TextLang)).Length;
        textLang = (TextLang)(((int)textLang - 1 + count) % count);

        UIManager.Instance.SetTextData();
    }

    public void NextVoiceLang()
    {
        int count = System.Enum.GetValues(typeof(VoiceLang)).Length;
        voiceLang = (VoiceLang)(((int)voiceLang + 1) % count);
    }

    public void PreviousVoiceLang()
    {
        int count = System.Enum.GetValues(typeof(VoiceLang)).Length;
        voiceLang = (VoiceLang)(((int)voiceLang - 1 + count) % count);
    }
}
