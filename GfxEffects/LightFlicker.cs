using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] Sprite lightOn;
    [SerializeField] Sprite lightOff;

    [Space]
    [SerializeField] SpriteRenderer flickerRenderer;

    [Space]
    [SerializeField] GameObject lightObject;

    [Space]
    [SerializeField] bool random;
    [SerializeField] float interval;

    public bool flickering;

    bool lightOnBool = true;

    //private void Start()
    //{
    //    StartCoroutine(Flicker());
    //}

    private void OnEnable()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            if (lightOnBool)
            {
                if (lightObject != null)
                    lightObject.SetActive(false);

                flickerRenderer.sprite = lightOff;
                lightOnBool = false;
            }
            else
            {
                if (lightObject != null)
                    lightObject.SetActive(true);

                flickerRenderer.sprite = lightOn;
                lightOnBool = true;
            }

            if (random)
            {
                yield return new WaitForSeconds(Random.Range(0.1f, interval));
            }
            else
            {
                yield return new WaitForSeconds(interval);
            }

            if (!flickering) break;
        }
    }
}
