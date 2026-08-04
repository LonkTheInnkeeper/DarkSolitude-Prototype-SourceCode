using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{
    [SerializeField] List<Animator> animators = new List<Animator>();
    [SerializeField] bool switchOn = false;
    [SerializeField] bool automaticTrigger = false;

    public void TriggerAnimation(string trigger)
    {
        foreach (var animator in animators)
        {
                animator.SetTrigger(trigger);
        }
    }
}
