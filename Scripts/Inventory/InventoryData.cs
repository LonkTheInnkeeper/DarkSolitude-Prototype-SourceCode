
using System.Collections.Generic;

public class InventoryData
{
    public List<string> items = new List<string>();

    public bool CheckItem(string item)
    {
        return items.Contains(item);
    }
}
