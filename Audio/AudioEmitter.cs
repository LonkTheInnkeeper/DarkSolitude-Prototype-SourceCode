using UnityEngine;

public class AudioEmitter : MonoBehaviour
{
    [SerializeField] AudioEvent audioEvent;

    public void Play()
    {
        AudioManager.Instance.sfx.Play(audioEvent, transform.position);
    }

    public void Stop()
    {
        AudioManager.Instance.StopAllSounds();
    }
}
