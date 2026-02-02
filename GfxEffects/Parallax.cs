using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> upper;
    [SerializeField] List<SpriteRenderer> lower;

    [SerializeField] Transform objectTransform;


    public float parallaxFactor = 0.2f; // 0 = statické, 1 = pohybuje se s kamerou
    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        for (int i = 0; i < upper.Count; i++)
        {
            ParallaxPos(upper[i].transform, parallaxFactor * (i + 1));
        }

        for (int i = 0; i < lower.Count; i++)
        {
            ParallaxPos(lower[i].transform, parallaxFactor * (i + 1) * -1);
        }

        ParallaxPos(objectTransform, parallaxFactor * -1);

        lastCamPos = cam.position;
    }

    void ParallaxPos(Transform background, float factor)
    {
        Vector3 delta = cam.position - lastCamPos;
        background.position += new Vector3(delta.x * factor, delta.y * factor, 0);
    }
}
