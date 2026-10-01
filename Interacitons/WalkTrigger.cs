using UnityEngine;
using UnityEngine.Events;

public class WalkTrigger : MonoBehaviour
{
    [SerializeField] string requiredState;
    [SerializeField] bool requiredStateValue;

    [Space]
    [SerializeField] string addState;
    [SerializeField] bool addStateValue;

    [Space]
    public UnityEvent walkEvent;
    GameManager gameMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Trigger enter");
            if (requiredState == null ||
                gameMan.GetWorldState(requiredState) == requiredStateValue)
            {
                Trigger();
            }
        }
    }

    public void Trigger()
    {
        print("Walk trigger");
        walkEvent.Invoke();
        GameManager.Instance.SetWorldstate(addState, addStateValue);
    }
}
