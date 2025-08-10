using UnityEngine;

public class WallsTrigger : MonoBehaviour
{
    [SerializeField] SpriteRenderer wallRenderer;
    [SerializeField] bool renderOn;

    private void Start()
    {
        wallRenderer.enabled = renderOn;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            renderOn = !renderOn;
            wallRenderer.enabled = renderOn;
        }
    }
}
