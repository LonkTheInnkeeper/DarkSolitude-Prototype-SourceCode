using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class UITools
{
    public static IEnumerator AlphaShadeRoutine(Image image, float newAlpha, float speed)
    {
        float time = 0f;

        while (time < speed)
        {
            time += Time.deltaTime;

            float a = Mathf.Lerp(image.color.a, newAlpha, time / speed);
            image.color = new Color(image.color.r, image.color.g, image.color.b, a);

            yield return null;
        }

        image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
    }

    public static IEnumerator AlphaShadeRoutine(TextMeshProUGUI text, float newAlpha, float speed)
    {
        float time = 0f;

        while (time < speed)
        {
            time += Time.deltaTime;

            float a = Mathf.Lerp(text.color.a, newAlpha, time / speed);
            text.color = new Color(text.color.r, text.color.g, text.color.b, a);

            yield return null;
        }

        text.color = new Color(text.color.r, text.color.g, text.color.b, newAlpha);
    }

    public static IEnumerator ScaleRectRoutine(RectTransform rect, Vector3 newScale, float speed)
    {
        float time = 0f;

        while (time < speed)
        {
            time += Time.deltaTime;
            
            rect.localScale = Vector3.Lerp(rect.localScale, newScale, time / speed);

            yield return null;
        }

        rect.localScale = newScale;
    }

}
