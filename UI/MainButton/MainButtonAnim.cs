using UnityEngine;
using UnityEngine.EventSystems;

public class MainButtonAnim : MonoBehaviour, IUITrigger
{
    [Header("Animators")]
    [SerializeField] Animator mainAnimator;
    [SerializeField] Animator buttonAnimator;

    [Header("Trigger objects")]
    [SerializeField] GameObject smallTrigger;
    [SerializeField] GameObject bigTrigger;
    [Space]
    [SerializeField] GameObject button1;
    [SerializeField] GameObject button2;
    [SerializeField] GameObject button3;
    [SerializeField] GameObject button4;

    [Space]
    [SerializeField] RectTransform mousePoint;
    [SerializeField] float collapseDistance;
    [SerializeField] float openDistance;

    bool collapsed = true;

    string smallTriggerName;
    string bigTriggerName;

    string button1Name;
    string button2Name;
    string button3Name;
    string button4Name;

    private void Start()
    {
        smallTriggerName = smallTrigger.GetComponent<UICursorTrigger>().triggerName;
        bigTriggerName = bigTrigger.GetComponent<UICursorTrigger>().triggerName;

        button1Name = button1.GetComponent<UICursorTrigger>().triggerName;
        button2Name = button2.GetComponent<UICursorTrigger>().triggerName;
        button3Name = button3.GetComponent<UICursorTrigger>().triggerName;
        button4Name = button4.GetComponent<UICursorTrigger>().triggerName;

        bigTrigger.SetActive(false);
    }

    private void Update()
    {
        MainButtonCollapse();
    }

    private void MainButtonCollapse()
    {
        float distance = Vector2.Distance(GetComponent<RectTransform>().localPosition, mousePoint.localPosition);

        if (distance > collapseDistance && !collapsed)
        {
            mainAnimator.SetTrigger("Close");
            collapsed = true;
        }

        if (distance < openDistance && collapsed)
        {
            mainAnimator.SetTrigger("Open");
            collapsed = false;
        }
    }

    public void Trigger(bool trigger, string name)
    {
        if (trigger)
        {
            buttonAnimator.SetTrigger(name + "On");
        }

        else if (!trigger)
        {
            buttonAnimator.SetTrigger(name + "Off");
        }

        else
        {
            print("UI Main Button trigger does not exist");
        }
    }
}
