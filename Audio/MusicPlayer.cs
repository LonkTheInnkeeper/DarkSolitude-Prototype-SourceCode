using System;
using UnityEngine;

[Serializable]
public class MusicPlayer
{
    AudioSource source;
    Transform parent;

    public MusicPlayer(Transform parent)
    {
        this.parent = parent;
        source = CreateSource();
    }

    AudioSource CreateSource()
    {
        GameObject obj = new GameObject("MusicSource");
        obj.transform.SetParent(parent);
        AudioSource source = obj.AddComponent<AudioSource>();

        return source;
    }

    public void Play(AudioEvent audioEvent)
    {
        if (audioEvent == null) return;
        if (source == null) CreateSource();

        ConfigureSource(source, audioEvent);
        source.Play();
    }

    void ConfigureSource(AudioSource audioSource, AudioEvent audioEvent)
    {
        audioSource.clip = audioEvent.GetClip();
        audioSource.volume = audioEvent.volume;
        audioSource.pitch = 1;
        audioSource.loop = audioEvent.loop;
        audioSource.spatialBlend = 0;
    }
}
