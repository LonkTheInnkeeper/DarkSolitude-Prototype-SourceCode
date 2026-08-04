using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameObjectFlicker : MonoBehaviour
{
    [SerializeField] float maxInterval;
    [SerializeField] GameObject gObject;

    public void Start()
    {
        StartCoroutine(FlickerCoroutine());
    }

    IEnumerator FlickerCoroutine()
    {

        while (true)
        {
            float rng = Random.Range(0, maxInterval);

            gObject.SetActive(!gObject.activeInHierarchy);

            yield return new WaitForSeconds(rng);
        }
    }
}
