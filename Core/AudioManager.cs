using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource playerAudioSource;

    [Header("SFX")]
    public AudioSource sfxAudioSource1;
    public AudioSource sfxAudioSource2;

    [Header("UI")]
    public AudioSource uiAudioSource;
    public AudioSource uiAudioSource2;

    [Header("Story")]
    public AudioSource voiceBackground;
    public AudioSource dialogueVoiceSource;

    [Space]
    public List<AudioClip> itemPickups;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    [Header("UI sound")]
    public AudioClip diaogueSellect;
    public AudioClip diaogueHover;

    public AudioDatabase database;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        database = GetComponent<AudioDatabase>();
    }

    public void PlaySFX(string sfxName)
    {
        AudioClip clip = database.sfxClips.FirstOrDefault(clip => clip.name == sfxName);

        if (clip == null) return;

        if (!sfxAudioSource1.isPlaying)
        {
            sfxAudioSource1.clip = clip;
            sfxAudioSource1.Play();
        }
        else
        {
            sfxAudioSource2.clip = clip;
            sfxAudioSource2.Play();
        }

    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        if (!sfxAudioSource1.isPlaying)
        {
            sfxAudioSource1.clip = clip;
            sfxAudioSource1.Play();
        }
        else
        {
            sfxAudioSource2.clip = clip;
            sfxAudioSource2.Play();
        }
    }

    public void PlayUI(string uiName)
    {
        AudioClip clip = database.uiClips.FirstOrDefault(clip => clip.name == uiName);

        if (clip == null) return;

        if (!uiAudioSource.isPlaying)
        {
            uiAudioSource.clip = clip;
            uiAudioSource.Play();
        }
        else
        {
            uiAudioSource2.clip = clip;
            uiAudioSource2.Play();
        }
    }

    public void PlayUI(AudioClip clip)
    {
        if (clip == null) return;

        uiAudioSource.PlayOneShot(clip);
    }

    public void FadeSound()
    {
        StartCoroutine(FadeSoundRoutine(sfxAudioSource1));
        StartCoroutine(FadeSoundRoutine(sfxAudioSource2));
        StartCoroutine(FadeSoundRoutine(uiAudioSource));
        StartCoroutine(FadeSoundRoutine(uiAudioSource2));
        StartCoroutine(FadeSoundRoutine(playerAudioSource));
        StartCoroutine(FadeSoundRoutine(voiceBackground));
        StartCoroutine(FadeSoundRoutine(dialogueVoiceSource));
    }

    IEnumerator FadeSoundRoutine(AudioSource source)
    {
        if (source != null)
        {
            float startVolume = source.volume;
            float duration = 1;

            float time = 0f;

            while (time < duration)
            {
                source.volume = Mathf.Lerp(
                    startVolume,
                    0f,
                    time / duration
                );

                time += Time.deltaTime;

                yield return null;
            }

            source.volume = 0f;
        }
        else
            yield return null;
    }
}
