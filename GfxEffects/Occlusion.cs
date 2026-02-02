using UnityEngine;

public class Occlusion : MonoBehaviour
{
    [SerializeField] Transform occlusionPoint;
    [SerializeField] Color occlusionColor;
    [SerializeField] Color baseColor;

    SpriteRenderer occlusionSprite;
    Transform player;

    private void Start()
    {
        occlusionSprite = GetComponent<SpriteRenderer>();
        player = GameManager.Instance.player.transform;
    }

    private void Update()
    {
        SetOcclusion();
    }

    void SetOcclusion()
    {
        if (occlusionPoint.position.z < player.position.z) 
        {
            occlusionSprite.color = baseColor;
        }
        else
        {
            occlusionSprite.color = occlusionColor;
        }
    }
}
