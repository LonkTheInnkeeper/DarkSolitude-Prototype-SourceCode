using UnityEngine;

public class Occlusion : MonoBehaviour
{
    [SerializeField] Transform occlusionPoint;

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
            occlusionSprite.enabled = true;
        }
        else
        {
            occlusionSprite.enabled = false;
        }
    }
}
