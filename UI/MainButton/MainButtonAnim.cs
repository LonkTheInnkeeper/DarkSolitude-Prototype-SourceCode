using UnityEngine;
using UnityEngine.EventSystems;

public class MainButtonAnim : MonoBehaviour, IUITrigger
{
    [Header("Animators")]
    [SerializeField] Animator mainAnimator;
    //[SerializeField] Animator buttonAnimator;

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

    [Header("Audio")]
    [SerializeField] string menuUp;
    [SerializeField] string menuDown;
    [SerializeField] string buttonUp;

    bool collapsed = true;

    [Space]
    public string smallTriggerName;
    public string bigTriggerName;

    [Space]
    public string button1Name;
    public string button2Name;
    public string button3Name;
    public string button4Name;

    private void Start()
    {        bigTrigger.SetActive(false);
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
            AudioManager.Instance.PlayUI(menuDown);
        }

        if (distance < openDistance && collapsed)
        {
            mainAnimator.SetTrigger("Open");
            AudioManager.Instance.PlayUI(menuUp);
            collapsed = false;
        }
    }

    public void CursorOnUI(bool trigger)
    {
        //if (trigger)
        //{
        //    buttonAnimator.SetTrigger(name + "On");
        //}

        //else if (!trigger)
        //{
        //    buttonAnimator.SetTrigger(name + "Off");
        //}

        //else
        //{
        //    print("UI Main Button trigger does not exist");
        //}
    }
}
