using System;
using System.Collections.Generic;
using UnityEngine;

public class Hallway1 : MonoBehaviour, ILocation
{
    [SerializeField] string id;

    public void ApplyState()
    {
    }

    public string GetID()
    {
        return id;
    }

    public void TriggerStoryActions(List<string> actions)
    {
        throw new NotImplementedException();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = Guid.NewGuid().ToString();
        }
    }
#endif
}
