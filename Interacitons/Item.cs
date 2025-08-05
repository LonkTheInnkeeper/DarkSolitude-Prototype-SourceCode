using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] List<Transform> interactionPoints;
    [SerializeField] ItemScriptable item;
    [SerializeField] bool hideItem;

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

        if (inventoryMan.activeItem != null)
        {
            inventoryMan.inventory.ReturnItem();
            return;
        }

        inventoryMan.inventory.AddItem(item);

        if (hideItem) gameObject.SetActive(false);
    }
}
