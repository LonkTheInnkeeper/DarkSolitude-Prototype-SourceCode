using UnityEngine;
using UnityEngine.Events;

public class ScreenShadeRunner : MonoBehaviour
{
    public UnityEvent shadeEvent;

    public void TriggerShade()
    {
        ScreenShade shade = FindAnyObjectByType<ScreenShade>();

        shade.shadeAction = shadeEvent;
        shade.GetComponent<Animator>().SetTrigger("Action");
    }
}
