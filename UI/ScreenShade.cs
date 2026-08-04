using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenShade : MonoBehaviour
{
    [SerializeField] float shadeSpeed;
    [SerializeField] Image shade;

    public UnityEvent shadeAction;
    Animator animator;

    public enum ShadeType
    {
        Action,
        ShadeOn,
        ShadeOff
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerShade(UnityEvent shadeEvent, ShadeType shadeType)
    {
        if (shadeEvent == null)
        {
            shadeAction = null;
        }
        else
        {
            shadeAction = shadeEvent;
        }

        if (animator == null)
            animator = GetComponent<Animator>();

        animator.SetTrigger(shadeType.ToString());
    }

    public void ShadeOnAction(System.Action action)
    {
        StartCoroutine(ShadeOnActionRoutine(action));
    }

    public void ShadeOn()
    {
        StartCoroutine(ShadeRoutine(0, 1, shadeSpeed));
    }

    public void ShadeOff()
    {
        StartCoroutine(ShadeRoutine(1, 0, shadeSpeed));
    }

    IEnumerator ShadeOnActionRoutine(System.Action action)
    {
        yield return StartCoroutine(ShadeRoutine(0, 1, shadeSpeed));

        action?.Invoke();
    }

    IEnumerator ShadeRoutine(float startAlpha, float newAlpha, float speed)
    {
        shade.color = new Color(0, 0, 0, startAlpha);

        float time = 0f;

        while (time < speed)
        {
            time += Time.deltaTime;

            float a = Mathf.Lerp(shade.color.a, newAlpha, time / speed);
            shade.color = new Color(0, 0, 0, a);

            yield return null;
        }

        shade.color = new Color(0, 0, 0, newAlpha);
    }

    public void ShadeAction()
    {
        shadeAction.Invoke();
    }

    private void OnEnable()
    {
        GameEvents.OnLocationLoad += ShadeOff;
    }

    private void OnDisable()
    {
        GameEvents.OnLocationLoad -= ShadeOff;
    }
}
