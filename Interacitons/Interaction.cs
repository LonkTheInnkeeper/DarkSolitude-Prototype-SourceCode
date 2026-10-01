using System.Collections.Generic;
using UnityEngine;

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

    bool IsEnabled()
    {
        bool interactionsSum = true;

        foreach (var interaction in interactions)
        {
            if (!interaction.IsAvailable())
            {
                interactionsSum = false;
                break;
            }
        }

        if (!interactionsSum || !eventAvailable) return false;
        else return true;
    }

    void ToggleHint()
    {
        if (IsEnabled())
            UIManager.Instance.hintsHandler.SpawnHint(transform.position, HintUI.HintColor.Orange);
    }

    private void OnEnable()
    {
        GameEvents.OnToggleHints += ToggleHint;
    }

    private void OnDisable()
    {
        GameEvents.OnToggleHints -= ToggleHint;
    }
}
