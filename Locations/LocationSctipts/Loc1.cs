using System;
using UnityEngine;

public class Loc1 : MonoBehaviour, ILocation
{
    [SerializeField] string id;

    public string GetID()
    {
        return id;
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
