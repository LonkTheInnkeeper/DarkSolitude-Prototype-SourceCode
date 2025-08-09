
using System.Collections.Generic;
using Unity.Mathematics;

public class PlayerData
{
    public List<string> inventory = new List<string>();
    public List<string> eventKeys = new List<string>();

    public int currentLocation = 0;

    public float positionX;
    public float positionZ;

    public List<string> GetAllItems()
    {
        return inventory;
    }

    public void SaveItems(List<string> items)
    {
        inventory = new List<string>(items);
    }

    public bool HasEventKey(string key)
    {
        return eventKeys.Contains(key);
    }

    public void StoreEventKey(string key) 
    {
        eventKeys.Add(key);
    }

    public void SetCurrentLocation(int location)
    {
        this.currentLocation = location;
    }

    public void SavePosition(float x, float z)
    {
        this.positionX = x;
        this.positionZ = z;
    }

    public float2 GetPosition()
    {
        return new float2(positionX, positionZ);
    }
}
