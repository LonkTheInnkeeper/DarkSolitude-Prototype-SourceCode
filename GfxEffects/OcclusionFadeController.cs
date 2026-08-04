using UnityEngine;

public class OcclusionFadeController : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        player = GameManager.Instance.player.transform;
    }

    void Update()
    {
        Shader.SetGlobalVector("_PlayerPosition", player.position);
    }
}
