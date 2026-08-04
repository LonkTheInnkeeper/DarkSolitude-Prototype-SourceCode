using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UICursorTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    IUITrigger uiTrigger;

    public UnityEvent enterEvent;
    public UnityEvent exitEvent;

    private void Start()
    {
        uiTrigger = GetComponent<IUITrigger>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (uiTrigger != null)
            uiTrigger.CursorOnUI(true);

        enterEvent.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (uiTrigger != null)
            uiTrigger.CursorOnUI(false);

        exitEvent.Invoke();
    }
}
