using UnityEngine;

public class UIMousePoint : MonoBehaviour
{
    RectTransform point;

    private void Start()
    {
        point = GetComponent<RectTransform>();
    }

    void Update()
    {
        point.position = Input.mousePosition;
    }
}
