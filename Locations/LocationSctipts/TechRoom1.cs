using System.Collections.Generic;
using UnityEngine;

public class TechRoom1 : MonoBehaviour, ILocation
{
    [SerializeField] int index;
    LocationData locationData;

    [Header("Levichair closeup")]
    [SerializeField] SpriteRenderer chairRenderer;
    [SerializeField] List<Sprite> chairCloseups;
    [SerializeField] Transform chairPosition;

    [Space]
    [SerializeField] GameObject cameras;

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

    public void SetChairCoseup(int index)
    {
        chairRenderer.sprite = chairCloseups[index];
    }

    public void SwitchToCloseup(bool closeup)
    {
        GameManager.Instance.closeupState = closeup;

        if (closeup)
        {
            cameras.transform.position = chairPosition.position;
        }
    }
}
