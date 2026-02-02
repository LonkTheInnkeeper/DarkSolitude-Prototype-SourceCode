using UnityEngine;

public class Loc1 : MonoBehaviour, ILocation
{
    [SerializeField] int index;
    LocationData locationData;

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
}
