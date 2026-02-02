using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float yOffset;

    void Update()
    {
        if (!GameManager.Instance.closeupState)
            FollowTarget(target);
    }

    void FollowTarget(Transform target)
    {
        transform.position = target.position + new Vector3(0, yOffset, 0);
    }
}
