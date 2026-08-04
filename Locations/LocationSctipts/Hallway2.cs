using System;
using System.Collections.Generic;
using UnityEngine;

public class Hallway2 : MonoBehaviour, ILocation
{
    public List<Item> items;

    [SerializeField] string id;
    AreaData locationData;

    [SerializeField] LightFlicker posterFlicker;
    [SerializeField] SpriteRenderer posterRenderer;
    [SerializeField] Sprite posterOff;

    void ApplyState()
    {

    }

    public string GetID()
    {
        return id;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
#endif

    public void ScewdriverPickup()
    {
        posterFlicker.flickering = false;
        posterRenderer.sprite = posterOff;
    }

    private void OnEnable()
    {
        ApplyState();
    }

    void ILocation.ApplyState()
    {
        ApplyState();
    }

    public void TriggerStoryActions(List<string> actions)
    {
    }
}
