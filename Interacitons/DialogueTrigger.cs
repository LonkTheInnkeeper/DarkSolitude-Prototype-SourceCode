using System;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] TextAsset textAsset;
    [SerializeField] bool diary;

    public void TriggerDialogue()
    {
        if (!diary)
        {
            StoryManager.Instance.StartDialogue(textAsset.name);
        }
        else
        {
            StoryManager.Instance.StartDiary(textAsset.name);
        }

        SwitchAnimation switchAnimation = GetComponent<SwitchAnimation>();

        if (switchAnimation != null) 
        {
            StoryManager.Instance.exitAnimation = switchAnimation;
        }
    }

    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
