using UnityEngine;
using UnityEngine.EventSystems;

public class UICursorTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string triggerName;

    [SerializeField] GameObject triggerObject;

    IUITrigger uiTrigger;

    private void Start()
    {
        uiTrigger = triggerObject.GetComponent<IUITrigger>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        uiTrigger.Trigger(true, triggerName);
        //print(triggerName + " trigger entered");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiTrigger.Trigger(false, triggerName);
        //print(triggerName + " trigger exited");
    }
}
