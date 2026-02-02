using System.Collections.Generic;
using UnityEngine;

public class SwitchAnimation : MonoBehaviour
{
    [SerializeField] List<Animator> animators = new List<Animator>();
    [SerializeField] bool switchOn = false;
    [SerializeField] bool automaticTrigger = false;

    public void TriggerAnimation()
    {
        print("Triggering animation");

        if (!automaticTrigger) return;

        //foreach (var animator in animators)
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).length >
        //        animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        //    {
        //        return;

        //    }
        //}

        foreach (var animator in animators)
        {
            if (switchOn)
            {
                print("Trigger close");
                animator.SetTrigger("Close");
            }
            else
            {
                print("Trigger open");
                animator.SetTrigger("Open");
            }
        }
        switchOn = !switchOn;
    }
}
