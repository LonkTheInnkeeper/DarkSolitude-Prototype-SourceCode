using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public float runningDelay;
    public float walkingDelay;

    Movement movement;
    AudioEmitter emitter;

    private void Start()
    {
        emitter = GetComponent<AudioEmitter>();
        movement = GetComponent<Movement>();
        StartCoroutine(Steps());
    }

    IEnumerator Steps()
    {
        while (true)
        {
            if (movement.currentPlayerState == Movement.PlayerState.Running)
            {
                emitter.Play();
                yield return new WaitForSeconds(runningDelay);
            }
            else if (movement.currentPlayerState == Movement.PlayerState.Walking)
            {
                emitter.Play();
                yield return new WaitForSeconds(walkingDelay);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void OnEnable()
    {
        //StartCoroutine(Steps());
    }
}
