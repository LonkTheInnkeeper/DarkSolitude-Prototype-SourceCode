using UnityEngine;

public class CubeSpin : MonoBehaviour
{
    [SerializeField] float cubeSpinSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * cubeSpinSpeed * Time.deltaTime);
    }
}
