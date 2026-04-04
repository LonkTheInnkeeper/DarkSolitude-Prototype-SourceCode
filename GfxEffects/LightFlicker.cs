using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class LightFlicker : MonoBehaviour, ISavable
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

    private void Start()
    {
        ApplyState();
    }

    private void OnEnable()
    {
        ApplyState();
    }

    public void ApplyState()
    {
        print("Checking state");

        if (GameManager.Instance.CheckWorldState("screwdriver_picked"))
        {
            print("Check");
            flickerRenderer.sprite = lightOff;
            return;
        }

        print("Start flicker");

        StartCoroutine(Flicker());

    }

    IEnumerator Flicker()
    {
        while (true)
        {
            print("Coroutine start");

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
