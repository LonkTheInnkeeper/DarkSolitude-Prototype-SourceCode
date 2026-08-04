using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    [SerializeField] Sprite lockedSprite;
    [SerializeField] Sprite unlockedSprite;
    [SerializeField] List<SpriteRenderer> renderers;
    [SerializeField] Animator lockAnimator;

    private void Start()
    {
        lockAnimator = GetComponent<Animator>();
    }

    public void SetDoorLock(bool unlocked)
    {
        foreach (var renderer in renderers)
        {
            if (unlocked)
                renderer.sprite = unlockedSprite;
            else
                renderer.sprite = lockedSprite;
        }
    }

    public void SetDoorsOn(bool on)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = on;
        }
    }

    public void ToggleLockAnimation(string trigger)
    {
        if (lockAnimator == null)
            lockAnimator.GetComponent<Animator>();

        lockAnimator.SetTrigger(trigger);
    }
}
