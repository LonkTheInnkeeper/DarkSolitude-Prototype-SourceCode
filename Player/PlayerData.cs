
using System;
using System.Collections.Generic;
using Unity.Mathematics;

public class PlayerData
{
    public List<AreaData> locations = new List<AreaData>();

    public int currentLocation = 0;

    public float positionX;
    public float positionZ;

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

    public void SaveLocations(List<AreaData> locations)
    {
        this.locations = new List<AreaData>(locations);
    }
}
