using System;
using System.Collections;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] GameObject tutorialUI;
    [SerializeField] float duration;
    [SerializeField] string tutorialState;

    private void Start()
    {
        tutorialUI.SetActive(false);
    }

    public void ToggleTutorial()
    {
        print("Starting tutorial");
        StartCoroutine(TutorialRoutine());
    }

    IEnumerator TutorialRoutine()
    {
        tutorialUI.SetActive(true);

        Animator animator = tutorialUI.GetComponent<Animator>();

        animator.SetTrigger("Open");
        yield return new WaitForSeconds(duration);
        animator.SetTrigger("Close");
        yield return new WaitForSeconds(1);
        tutorialUI.SetActive(false);

        GameManager.Instance.SetWorldstate(tutorialState, true);
    }
}
