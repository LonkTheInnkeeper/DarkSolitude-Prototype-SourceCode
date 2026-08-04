using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioSteps : MonoBehaviour
{
    public AudioSource audioSource;

    public List<AudioClip> steps;
    public float delay;

    private void Start()
    {
        StartCoroutine(Steps());
    }

    IEnumerator Steps()
    {
        while (true)
        {
            AudioClip clip = steps[Random.Range(0, steps.Count)];

            if (GameManager.Instance.playerState == GameManager.PlayerState.Running)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }

            yield return new WaitForSeconds(delay);
        }
    }

    private void OnEnable()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        //StartCoroutine(Steps());
    }
}
