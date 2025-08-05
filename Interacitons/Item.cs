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
        UIManager uiMan = UIManager.Instance;

        InventoryManager.Instance.inventory.AddItem(item);

        if (hideItem) gameObject.SetActive(false);
    }
}
