using System;
using UnityEngine;

[Serializable]
public class SFXPlayer
{
    AudioSourcePool pool;

    public SFXPlayer(Transform sourceParent)
    {
        pool = new AudioSourcePool(sourceParent, "SFXSource");
    }

    public void Play(AudioEvent audioEvent, Vector3 position)
    {
        if (audioEvent == null) return;

        AudioSource audioSource = pool.Get();

        if (audioSource == null) return;

        ConfigureSource(audioSource, audioEvent);
        audioSource.transform.position = position;
        audioSource.Play();
    }

    void ConfigureSource(AudioSource audioSource, AudioEvent audioEvent)
    {
        audioSource.clip = audioEvent.GetClip();
        audioSource.volume = audioEvent.volume;
        audioSource.pitch = audioEvent.GetPitch();
        audioSource.loop = audioEvent.loop;
        audioSource.spatialBlend = audioEvent.spatialBlend;
    }

    public void StopAll()
    {
        pool.StopAll();
    }
}
