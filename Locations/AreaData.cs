using System.Collections.Generic;

public class AreaData
{
    public string areaName;
    public string currentLocationID;

    public Dictionary<string, bool> worldStates = new Dictionary<string, bool>();
    public Dictionary<string, int> closeups = new Dictionary<string, int>();

    public void AddKey(string key, bool state)
    {
        if (!worldStates.ContainsKey(key))
        {
            worldStates.Add(key, state);
            return;
        }

        worldStates[key] = state;
    }

    public bool CheckKey(string key)
    {
        return worldStates.ContainsKey(key) && worldStates[key];
    }

    public void RemoveKey(string key)
    {
        if (worldStates.ContainsKey(key)) worldStates.Remove(key);
    }

    public int GetCloseupFrame(string key)
    {
        if (!closeups.ContainsKey(key))
        {
            closeups.Add(key, 0);
        }

        return closeups[key];
    }

    public void SetCloseup(string key, int frame)
    {
        closeups[key] = frame;
    }
}
