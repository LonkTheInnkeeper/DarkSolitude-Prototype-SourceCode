using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] Animator doorAnimator;
    [SerializeField] string openTrigger;
    [SerializeField] string closeTrigger;

    [Header("Door lock")]
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite unlockedSprite;
    [SerializeField] List<SpriteRenderer> renderers;
    [SerializeField] Animator lockAnimator;

    [Space]
    [SerializeField] AudioSource audioSource;

    [Space]
    [SerializeField] bool lockFlicker;
    [SerializeField] float flickerInterval;
    [SerializeField] float flickerAlphaRange;

    [Space]
    public bool doorUnlocked;
    public Transform spawnPoint;

    private void Start()
    {
        SetDoorLock(doorUnlocked);

        if (lockFlicker)
        {
            StartCoroutine(Flicker());
        }
    }

    public bool ToggleDoors(bool open)
    {
        AudioManager audioMan = AudioManager.Instance;

        if (!doorUnlocked)
        {
            print("Doors are locked");
            return false;
        }

        print("Toggling door");

        if (!open)
        {
            print("Closing door");
            doorAnimator.SetTrigger(closeTrigger);
            ToggleLockAnimation("Open");

            audioSource.clip = audioMan.doorClose;
            audioSource.Play();
        }
        else
        {
            print("Opening door");
            doorAnimator.SetTrigger(openTrigger);
            ToggleLockAnimation("Close");

            audioSource.clip = audioMan.doorOpen;
            audioSource.Play();
        }

        return true;
    }

    public void SetDoorLock(bool unlocked)
    {
        doorUnlocked = unlocked;

        foreach (var renderer in renderers)
        {
            if (doorUnlocked)
                renderer.sprite = unlockedSprite;
            else
                renderer.sprite = lockedSprite;
        }
    }

    IEnumerator Flicker()
    {
        float alpha = flickerAlphaRange;

        while (true)
        {
            foreach (var renderer in renderers)
            {
                Color c = renderer.color;
                c.a = Mathf.Lerp(c.a, c.a - alpha, 0.5f); ;
                renderer.color = c;
            }

            yield return new WaitForSeconds(Random.Range(0.1f, flickerInterval));

            foreach (var renderer in renderers)
            {
                Color c = renderer.color;
                c.a = Mathf.Lerp(c.a, c.a + alpha, 0.5f); ;
                renderer.color = c;
            }

            yield return new WaitForSeconds(Random.Range(0.1f, flickerInterval));

            alpha = Random.Range(0.1f, flickerAlphaRange);
        }
    }

    public void ToggleLockAnimation(string trigger)
    {
        if (lockAnimator == null)
            lockAnimator.GetComponent<Animator>();

        lockAnimator.SetTrigger(trigger);
        print("Lock animation " + trigger);
    }
}
