using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFlicker : MonoBehaviour
{
    [SerializeField] float maxInterval = 1;

    Image image;
    Color baseColor;

    public void Start()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
    }

    IEnumerator FlickerCoroutine()
    {
        while (true)
        {
            image.color = new Color(1, 1, 1, (baseColor.a / 1.1f));

            yield return new WaitForSeconds(GetRandomFloat());

            image.color = baseColor;

            yield return new WaitForSeconds(GetRandomFloat());
        }
    }

    float GetRandomFloat()
    {
        return Random.Range(0.1f, maxInterval);
    }

    private void OnEnable()
    {
        image = GetComponent<Image>();
        baseColor = image.color;
        StartCoroutine(FlickerCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        image.color = baseColor;
    }
}
