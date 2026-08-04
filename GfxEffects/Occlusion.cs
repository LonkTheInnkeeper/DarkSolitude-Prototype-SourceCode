using System.Collections;
using UnityEngine;

public class Occlusion : MonoBehaviour
{
    [SerializeField] Transform occlusionPoint;

    SpriteRenderer occlusionSprite;
    Transform player;

    public float offsetY = -2.15f;
    public float offsetZ = -3f;
    public int offsetMultiplier = 2;

    Vector3 position;

    private void Start()
    {
        position = new Vector3(0, offsetY * offsetMultiplier, offsetZ * offsetMultiplier);

        occlusionSprite = GetComponent<SpriteRenderer>();
        player = GameManager.Instance.player.transform;

        MoveToCamera();
        StartCoroutine(Move());
    }

    private void Update()
    {
        SetOcclusion();
    }

    void SetOcclusion()
    {
        if (occlusionPoint == null) return;

        if (occlusionPoint.position.z < player.position.z)
        {
            //if (transform.position != position)
            //    transform.position = position;

            occlusionSprite.enabled = true;
        }
        else
        {
            occlusionSprite.enabled = false;
        }
    }

    IEnumerator Move()
    {
        yield return new WaitForEndOfFrame();
        MoveToCamera();
    }

    void MoveToCamera()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            transform.position = position;
        }
        else
        {
            rectTransform.anchoredPosition3D = position;
        }
    }
}
