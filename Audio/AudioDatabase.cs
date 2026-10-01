using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    [SerializeField] List<AudioElement> audioElements = new List<AudioElement>();

    public AudioClip Get(string id)
    {
        return audioElements.FirstOrDefault(el => el.id == id).audioClip;
    }
}
