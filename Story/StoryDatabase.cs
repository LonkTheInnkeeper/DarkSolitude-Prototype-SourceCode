using System.Collections.Generic;
using UnityEngine;

public class StoryDatabase : MonoBehaviour
{
    public List<TextAsset> textAssets;

    public TextAsset GetDialogue(string name)
    {
        string localisation = GameManager.Instance.settingsData.textLang.ToString();
        string dialogueName = $"{name}";

        TextAsset dialogue = Resources.Load<TextAsset>($"Story/Dialogues/{name}/{name}");

        if (dialogue == null)
        {
            Debug.LogWarning($"Dialogue {name} does not exist");
            return null;
        }

        return dialogue;
    }

    public TextAsset GetComment(string name)
    {
        string localisation = GameManager.Instance.settingsData.textLang.ToString();
        string commentName = $"{name}_{localisation}";

        TextAsset comment = Resources.Load<TextAsset>($"Story/Comments/{name}/{localisation}/{commentName}");

        if (comment == null)
        {
            Debug.LogWarning($"Comment {name} does not exist");
            return null;
        }

        return comment;
    }

    public TextAsset GetDiary(string name)
    {
        string localisation = GameManager.Instance.settingsData.textLang.ToString();
        string diaryName = $"{name}_{localisation}";

        TextAsset diary = Resources.Load<TextAsset>($"Story/Diaries/{name}/{localisation}/{diaryName}");

        if (diary == null)
        {
            Debug.LogWarning($"Diary {name} does not exist");
            return null;
        }

        return diary;
    }

    public string GetInfoText(string key)
    {
        string localisation = GameManager.Instance.settingsData.textLang.ToString();
        InfoTextData infoTexts = SaveLoadSystem.LoadInfoText(localisation);

        if (infoTexts == null)
        {
            Debug.LogWarning($"Infotext file {localisation} does not exist");
            return null;
        }

        if (!infoTexts.data.ContainsKey(key))
        {
            return $"## Key \"{key}\" at \"{localisation}\" is missing ##";
        }

        return infoTexts.data[key];
    }

    public AudioClip GetDialogueVoice(string dialogueName, string key)
    {
        string localisation = "cz";
        string clipName = $"{key}_{localisation}";
        AudioClip clip = Resources.Load<AudioClip>($"Story/Dialogues/{dialogueName}/{localisation}/{clipName}");

        if (clip == null)
        {
            Debug.LogWarning($"Audio clip {clipName} does not exist");
            return null;
        }

        return clip;
    }

    public AudioClip GetCommentVoice(string commentNane, string line)
    {
        string localisation = GameManager.Instance.settingsData.voiceLang.ToString();
        string clipName = $"{commentNane}_{localisation}_{line}";
        AudioClip clip = Resources.Load<AudioClip>($"Story/Comments/{commentNane}/{localisation}/{clipName}");

        if (clip == null)
        {
            Debug.LogWarning($"Audio clip {clipName} does not exist");
            return null;
        }

        return clip;
    }
}