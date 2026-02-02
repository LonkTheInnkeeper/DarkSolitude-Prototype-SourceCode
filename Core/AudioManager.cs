using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource playerAudioSource;
    [Space]
    public List<AudioClip> itemPickups;
    public AudioClip doorOpen;
    public AudioClip doorClose;


    private void Awake()
    {
        Instance = this;
    }
}
