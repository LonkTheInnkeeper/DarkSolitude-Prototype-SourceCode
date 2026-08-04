using System.Collections.Generic;
using UnityEngine;

public class AudioDatabase : MonoBehaviour
{
    public List<AudioClip> uiClips;
    public List<AudioClip> sfxClips;

    public AudioClip GetInventoryClip(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>($"Audio/UI/Inventory/{clipName}");

        if ( clip == null)
        {
            Debug.LogError($"Audio clip {clipName} does not exist");
            return null;
        }
        
        return clip;
    }
}
