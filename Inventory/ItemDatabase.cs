using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemDatabase : MonoBehaviour
{
    public List<ItemScriptable> items;

    public ItemScriptable GetItem(string name)
    {
        return items.FirstOrDefault(item => item.name == name);
    }

    public ItemScriptable GetItem(int index)
    {
        return items[index];
    }
}
