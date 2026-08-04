using UnityEngine;

public class MainMenuButton : MonoBehaviour, IUITrigger
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CursorOnUI(bool trigger)
    {
        if (trigger) animator.SetTrigger("Sellect");
        else animator.SetTrigger("Desellect");
    }
}
