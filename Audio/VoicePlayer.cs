using System;
using UnityEngine;

[Serializable]
public class VoicePlayer
{
    readonly AudioSource voiceSource;
    readonly AudioSource backgroundSource;
    readonly AudioDatabase database;

    public VoicePlayer(Transform parent, AudioDatabase database)
    {
        this.database = database;

        voiceSource = CreateSource("VoiceSource", parent);
        backgroundSource = CreateSource("VoiceBackgroundSource", parent);
    }

    AudioSource CreateSource(string name, Transform parent)
    {
        GameObject obj = new GameObject(name);
        obj.transform.SetParent(parent);

        AudioSource source = obj.AddComponent<AudioSource>();
        source.playOnAwake = false;
        source.spatialBlend = 0;

        return source;
    }

    public void PlayVoice(AudioClip clip)
    {
        if (clip == null)
            return;

        voiceSource.clip = clip;
        voiceSource.loop = false;
        voiceSource.Play();
    }

    public void PlayBackground(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        AudioClip clip = database.Get(id);

        if (clip == null)
            return;

        backgroundSource.clip = clip;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }

    public void StopVoice()
    {
        voiceSource.Stop();
    }

    public void StopBackground()
    {
        backgroundSource.Stop();
    }
}