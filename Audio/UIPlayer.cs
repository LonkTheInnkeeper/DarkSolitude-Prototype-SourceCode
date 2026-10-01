using System;
using UnityEngine;

[Serializable]
public class UIPlayer
{
    AudioSourcePool pool;
    AudioDatabase database;

    public UIPlayer(Transform sourceParent, AudioDatabase database) 
    {
        this.database = database;

        pool = new AudioSourcePool(sourceParent, "UISource");
    }

    public void Play(string id)
    {
        if (string.IsNullOrEmpty(id)) return;

        AudioSource source = pool.Get();
        AudioClip clip = database.Get(id);

        if (source == null || clip == null) return;

        source.clip = clip;
        source.Play();
    }

    public void StopAll()
    {
        pool.StopAll();
    }
}
