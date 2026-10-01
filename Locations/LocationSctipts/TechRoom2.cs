using System;
using System.Collections.Generic;
using UnityEngine;

public class TechRoom2 : MonoBehaviour, ILocation
{
    [SerializeField] string id;
    AreaData locationData;

    [Header("Closeup")]
    [SerializeField] Transform closeupPosition;

    [Space]
    [SerializeField] GameObject cameras;

    [Header("El. box")]
    [SerializeField] SpriteRenderer elBox;
    [SerializeField] Sprite closedRepaired;
    [SerializeField] Sprite closedBroken;
    [SerializeField] Sprite openRepaired;
    [SerializeField] Sprite openBroken;
    [SerializeField] AudioEmitter ambience;

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

    public void ApplyState()
    {
        bool open = GameManager.Instance.GetWorldState("ElBox_Open");
        bool repaired = !GameManager.Instance.GetWorldState("Loc1_Door1_NoPower");

        if (open && repaired)
        {
            elBox.sprite = openRepaired;
        }
        else if (open && !repaired)
        {
            elBox.sprite = openBroken;
        }
        else if (!open && repaired)
        {
            elBox.sprite = closedRepaired;
        }
        else if (!open && !repaired)
        {
            elBox.sprite = closedBroken;
        }
    }

    public void TriggerStoryActions(List<string> actions)
    {
        print("Story action");
        if (actions[0] == "BoxReset")
        {
            ApplyState();
        }
    }

    private void OnEnable()
    {
        ApplyState();

        ambience.Play();
    }

    private void OnDisable()
    {
        ambience.Stop();
    }
}
