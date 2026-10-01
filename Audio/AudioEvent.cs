using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptables/Audio Event")]
public class AudioEvent : ScriptableObject
{
    public AudioClip[] clips;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(-3f, 3f)]
    public float pitch = 1f;

    [Range(0, 3f)]
    public float randomPitchRange = 0f;

    [Space]
    public bool randomClip;
    public bool randomPitch;
    public bool loop;

    [Space]
    public AudioMixerGroup mixerGroup;

    [Range(0f, 1f)]
    public float spatialBlend = 1f;

    public AudioClip GetClip()
    {
        if (randomClip)
        {
            return clips[Random.Range(0, clips.Length)];
        }

        return clips[0];
    }

    public float GetPitch()
    {
        if (randomPitch)
            return Random.Range(pitch - randomPitchRange, pitch + randomPitchRange);

        return pitch;
    }
}
