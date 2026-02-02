using UnityEngine;
using UnityEngine.Events;

public class WalkTrigger : MonoBehaviour
{
    public UnityEvent walkEvent;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Walk trigger");
            walkEvent.Invoke();
        }
    }
}
