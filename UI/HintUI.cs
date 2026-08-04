using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{
    [SerializeField] Image image;
    [Space]
    [SerializeField] Sprite blue;
    [SerializeField] Sprite orange;

    Vector3 position;
    HintColor color;

    public enum HintColor
    {
        Blue,
        Orange
    }

    public void Init(Vector3 position, HintColor color, float refreshTime)
    {
        this.position = position;
        this.color = color;

        switch (color)
        {
            case HintColor.Blue:
                image.sprite = blue;
                break;

            case HintColor.Orange:
                image.sprite = orange;
                break;

            default:
                break;
        }

        Destroy(gameObject, refreshTime + 1);
    }

    void LateUpdate()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        GetComponent<RectTransform>().position = screenPos;
    }
}
