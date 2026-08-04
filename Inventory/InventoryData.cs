using System.Collections.Generic;
using UnityEngine;

public class InventoryData
{
    public Dictionary<int, string> items = new Dictionary<int, string>();

    public InventoryData() 
    {
        if (InventoryManager.Instance == null) return;

        for (int i = 0; i < InventoryManager.Instance.inventorySize; i++)
        {
            items.Add(i, null);
        }
    }

    public bool TryGetItem(int slot, out KeyValuePair<int, string> item)
    {
        if (items.TryGetValue(slot, out string value))
        {
            item = new KeyValuePair<int, string>(slot, value);
            return true;
        }

        item = default;
        return false;
    }
}
