using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaction : MonoBehaviour, IInteractable
{
    [SerializeField] List<Transform> interactionPoints;

    [Space]
    public bool eventAvailable = true;
    public bool disableAfterUse = true;

    [Space]
    public List<InteractionClass> interactions = new List<InteractionClass>();

    // Player navigation
    public List<Transform> GetInteractionPoints()
    {
        return interactionPoints;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Interaction logic
    public void Interact()
    {
        if (!eventAvailable)
            return;

        DisableEvent();

        foreach (var interaction in interactions) 
        {
            interaction.TriggerEvent();
        }
    }

    void DisableEvent()
    {
        if (disableAfterUse)
        {
            eventAvailable = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
