using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float yOffset;
    [SerializeField] float camSpeed;

    private void Start()
    {
        FocusOnPlayer();
    }

    void Update()
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Closeup &&
            !GameManager.Instance.closeupState)
            FollowTarget(target);
    }

    void FollowTarget(Transform target)
    {
        transform.position = Vector3.Lerp(
        transform.position,
        target.position + new Vector3(0, yOffset, 0),
        camSpeed * Time.deltaTime
    );
    }

    public void FocusOnPlayer()
    {
        transform.position = target.position;
    }
}
