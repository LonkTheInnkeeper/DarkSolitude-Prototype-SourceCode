using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{
    [SerializeField] List<Animator> animators = new List<Animator>();
    public void TriggerAnimation(string trigger)
    {
        foreach (var animator in animators)
        {
            animator.SetTrigger(trigger);
        }
    }
}
