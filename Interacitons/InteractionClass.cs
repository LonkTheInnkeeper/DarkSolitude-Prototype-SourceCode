using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class InteractionClass
{
    [SerializeField] ItemScriptable requiredItem;
    [SerializeField] string requiredState;
    [SerializeField] bool requiredStateValue;

    [Space]
    [SerializeField] bool removeItemAfterUse = true;

    [Space]
    [SerializeField] bool eventAvailable = true;
    [SerializeField] bool disableAfterUse;

    [Space]
    [SerializeField] string addState;
    [SerializeField] bool stateValue;

    public UnityEvent event_;

    public void TriggerEvent()
    {
        if (!eventAvailable) return;

        Debug.Log("Event triggered");

        InventoryManager inventoryMan = InventoryManager.Instance; 
        GameManager gameMan = GameManager.Instance;

        if ((requiredItem != null && inventoryMan.activeItem != requiredItem) ||
            (requiredState != string.Empty && !gameMan.CheckWorldState(requiredState) == requiredStateValue))
        {
            Debug.LogWarning("Interaction requirements not met");
            return;
        }

        if (removeItemAfterUse)
            inventoryMan.inventory.DesellectActiveItem();
        else
            inventoryMan.inventory.ReturnActiveItem();

        if (disableAfterUse)
            eventAvailable = false;

        gameMan.AddWorldstate(addState, stateValue);

        event_.Invoke();
    }

    public bool IsAwailable()
    {
        return eventAvailable;
    }
}
