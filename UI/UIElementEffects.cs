using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIElementEffects : MonoBehaviour
{
    [Header("Image alpha shade")]
    [SerializeField] float originImageAlpha;
    [SerializeField] float newImageAlpha;
    [SerializeField] float imageTransitionTime;
    [SerializeField] List<Image> images;

    [Header("Text alpha shade")]
    [SerializeField] float originTextAlpha;
    [SerializeField] float newTextAlpha;
    [SerializeField] float textTransitionTime;
    [SerializeField] List<TextMeshProUGUI> texts;

    private void Start()
    {
        if (images.Count == 0)
            images.Add(GetComponent<Image>());

        if (texts.Count == 0)
            texts.Add(GetComponent<TextMeshProUGUI>());
    }

    public void ImageAlphaShade()
    {
        foreach (var image in images)
        {
            if (image.gameObject.activeInHierarchy)
                StartCoroutine(UITools.AlphaShadeRoutine(image, newImageAlpha, imageTransitionTime));
        }
    }

    public void OriginImageAlpha()
    {
        foreach (var image in images)
        {
            if (image.gameObject.activeInHierarchy)
                StartCoroutine(UITools.AlphaShadeRoutine(image, originImageAlpha, imageTransitionTime));
        }
    }

    public void TextAlphaShade()
    {
        foreach (var text in texts)
        {
            if (text.gameObject.activeInHierarchy)
                StartCoroutine(UITools.AlphaShadeRoutine(text, newTextAlpha, textTransitionTime));
        }
    }

    public void OriginTextAlpha()
    {
        foreach (var text in texts)
        {
            if (text.gameObject.activeInHierarchy)
                StartCoroutine(UITools.AlphaShadeRoutine(text, originTextAlpha, textTransitionTime));
        }
    }

    public void SwitchToOriginImageAlpha()
    {
        foreach (var image in images)
        {
            if (image.gameObject.activeInHierarchy)
                image.color = new Color(image.color.r, image.color.g, image.color.b, originImageAlpha);
        }
    }

    public void SwitchToOriginTextAlpha()
    {
        foreach (var text in texts)
        {
            if (text.gameObject.activeInHierarchy)
                text.color = new Color(text.color.r, text.color.g, text.color.b, originTextAlpha);
        }
    }
}
