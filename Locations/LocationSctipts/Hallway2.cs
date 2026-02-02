using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Hallway2 : MonoBehaviour, ILocation
{
    public List<Item> items;

    [SerializeField] int index;
    LocationData locationData;

    [SerializeField] LightFlicker posterFlicker;
    [SerializeField] SpriteRenderer posterRenderer;
    [SerializeField] Sprite posterOff;

    public bool ContainsKey(string key)
    {
        return locationData.data.Contains(key);
    }

    public void AddKey(string key)
    {
        locationData.data.Add(key);
    }

    public LocationData GetData()
    {
        return locationData;
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetData(LocationData data)
    {
        if (data == null)
            locationData = new LocationData(index);
        else
            locationData = data;
    }

    public void ScewdriverPickup()
    {
        posterFlicker.flickering = false;
        posterRenderer.sprite = posterOff;
    }
}
