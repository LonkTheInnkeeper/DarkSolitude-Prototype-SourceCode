using System.Collections.Generic;

public class InteractionsData
{
    public List<string> eventKeys = new List<string>();

    public bool HasEventKey(string key)
    {
        return eventKeys.Contains(key);
    }
}
