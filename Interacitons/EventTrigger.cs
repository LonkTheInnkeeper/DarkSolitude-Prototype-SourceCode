using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] List<Transform> interactionPoints;
    [SerializeField] ItemScriptable item;
    [SerializeField] bool removeUsedItem;
    [SerializeField] string eventKey;

    public UnityEvent eventTrigger;

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
        InventoryManager inventoryMan = InventoryManager.Instance;
        InteractionsData interactionsData = GameManager.Instance.playerData.interactionsData;

        if (eventKey != string.Empty)
        {
            if (interactionsData.HasEventKey(eventKey))
            {
                print("This event has already been triggered");
                return;
            }
            else
            {
                print("Adding event key: " + eventKey);
                interactionsData.eventKeys.Add(eventKey);
            }
        }

        if (item == null)
        {
            print("Using event trigger");
            eventTrigger.Invoke();
            return;
        }

        if (inventoryMan.activeItem == item)
        {
            print("Using item to trigger: " + item.name);

            if (removeUsedItem)
            {
                inventoryMan.inventoryData.items.Remove(item.name);
            }

            inventoryMan.activeItem = null;
            eventTrigger.Invoke();
        }
        else
        {
            Debug.LogWarning("Trying to use wrong item");
            if (inventoryMan.activeItem != null)
            {
                inventoryMan.inventory.DesellectItem();
            }
        }
    }
}
