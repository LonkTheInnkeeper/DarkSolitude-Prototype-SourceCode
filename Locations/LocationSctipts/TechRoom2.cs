using System;
using UnityEngine;

public class TechRoom2 : MonoBehaviour, ILocation
{
    [SerializeField] string id;
    AreaData locationData;

    [Header("Closeup")]
    [SerializeField] Transform closeupPosition;

    [Space]
    [SerializeField] GameObject cameras;

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

    public void SwitchToCloseup(bool closeup)
    {
        GameManager.Instance.closeupState = closeup;

        if (closeup)
        {
            cameras.transform.position = closeupPosition.position;
        }
    }
}
