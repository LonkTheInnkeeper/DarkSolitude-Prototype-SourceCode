
using System;
using System.Collections.Generic;
using Unity.Mathematics;

public class PlayerData
{
    public int index;
    public string fileName = "Empty name";
    public string fileDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

    public List<string> inventory = new List<string>();
    public List<LocationData> locations = new List<LocationData>();

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

    public void UpdateFileDate()
    {
        fileDate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
    }

    public void SaveLocations(List<LocationData> locations)
    {
        this.locations = new List<LocationData>(locations);
    }
}
