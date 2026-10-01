using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool 
{
    List<AudioSource> audioSources = new List<AudioSource>();
    Transform parent;
    string sourceName;

    int minSources = 5;
    int maxSources = 15;

    public AudioSourcePool(Transform parent, string sourceName)
    {
        this.parent = parent;
        this.sourceName = sourceName;

        for (int i = 0; i < minSources; i++) 
        {
            CreateSource(sourceName);
        }
    }

    AudioSource CreateSource(string sourceName)
    {
        if (audioSources.Count >= maxSources) return null;

        GameObject obj = new GameObject(sourceName);
        obj.transform.SetParent(parent);
        AudioSource source = obj.AddComponent<AudioSource>();

        source.playOnAwake = false;
        audioSources.Add(source);

        return source;
    }

    public AudioSource Get()
    {
        foreach (var source in audioSources)
        {
            if (!source.isPlaying)
            {
                return source;
            }
        }

        return CreateSource(sourceName);
    }

    public void StopAll()
    {
        foreach(var source in audioSources)
        {
            source.Stop();
        }
    }
}
