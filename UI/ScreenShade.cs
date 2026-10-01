using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ScreenShade : MonoBehaviour
{
    [SerializeField] float shadeSpeed;
    [SerializeField] Image shade;

    public UnityEvent shadeAction;
    public UnityEvent clearEvent;
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

        ShadeOnEvent();
    }

    public void ShadeOnAction(System.Action action)
    {
        StartCoroutine(ShadeOnActionRoutine(action));
    }

    public void ShadeOnEvent()
    {
        StartCoroutine(ShadeEventRoutine(0, 1, shadeSpeed));
    }

    public void ShadeOffEvent()
    {
        StartCoroutine(ShadeEventRoutine(1, 0, shadeSpeed));
    }

    public void ShadeOff()
    {
        StartCoroutine(ShadeRoutine(1, 0, shadeSpeed));
    }

    public void ShadeOn()
    {
        StartCoroutine(ShadeRoutine(0, 1, shadeSpeed));
    }

    IEnumerator ShadeOnActionRoutine(System.Action action)
    {
        yield return StartCoroutine(ShadeEventRoutine(0, 1, shadeSpeed));

        action?.Invoke();
    }

    IEnumerator ShadeEventRoutine(float startAlpha, float newAlpha, float speed)
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

        if (shadeAction.GetPersistentEventCount() != 0)
            ShadeAction();

        shadeAction = clearEvent;
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

        shadeAction = clearEvent;
    }

    public void ShadeAction()
    {
        shadeAction.Invoke();
        shadeAction = clearEvent;
    }

    private void OnEnable()
    {
        GameEvents.OnLocationLoad += ShadeOffEvent;
    }

    private void OnDisable()
    {
        GameEvents.OnLocationLoad -= ShadeOffEvent;
    }
}
