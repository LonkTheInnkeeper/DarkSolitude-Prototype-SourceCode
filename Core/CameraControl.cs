using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        FollowTarget(target);
    }

    void FollowTarget(Transform target)
    {
        transform.position = target.position;
    }
}
