using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] List<Transform> interactionPoints;
    [SerializeField] ItemScriptable requiredItem;
    [SerializeField] bool removeUsedItem;
    [SerializeField] string eventKey;
    [SerializeField] int locationIndex;

    [SerializeField] bool eventAvailable = true;
    [SerializeField] bool disableAfterUse = true;

    public UnityEvent eventTrigger;
    public UnityEvent itemMissingEvent;

    public List<Transform> GetInteractionPoints()
    {
        return interactionPoints;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void Interact()
    {
        if (!eventAvailable) return;

        InventoryManager inventoryMan = InventoryManager.Instance;
        ILocation location = LocationManager.instance.GetLocation(locationIndex);

        if (eventKey != string.Empty)
        {
            if (location.ContainsKey(eventKey))
            {
                print("This event has already been triggered");
                return;
            }
            else
            {
                print("Adding event key: " + eventKey);
                LocationManager.instance.SaveEventKey(locationIndex, eventKey);
            }
        }

        if (requiredItem == null)
        {
            if (inventoryMan.activeItem != null)
            {
                inventoryMan.inventory.DesellectItem();
                return;
            }

            print("Using event trigger");
            EventTriggered();

            return;
        }

        if (inventoryMan.activeItem == requiredItem)
        {
            print("Using item to trigger: " + requiredItem.name);

            if (removeUsedItem)
            {
                inventoryMan.inventoryData.items.Remove(requiredItem.name);
            }
            else
            {
                inventoryMan.inventory.DesellectItem();
            }

            EventTriggered();
        }
        else
        {
            Debug.LogWarning("Trying to use wrong item");
            if (inventoryMan.activeItem != null)
            {
                inventoryMan.inventory.DesellectItem();
            }

            itemMissingEvent.Invoke();
        }
    }

    void EventTriggered()
    {
        ILocation location = LocationManager.instance.GetLocation(locationIndex);

        if (eventKey != string.Empty)
        {
            if (location.ContainsKey(eventKey))
            {
                print("This event has already been triggered");
                return;
            }
            else
            {
                print("Adding event key: " + eventKey);
                LocationManager.instance.SaveEventKey(locationIndex, eventKey);
            }
        }

        if (disableAfterUse)
        {
            eventAvailable = false;
            GetComponent<Collider>().enabled = false;
        }

        eventTrigger.Invoke();
    }
}
