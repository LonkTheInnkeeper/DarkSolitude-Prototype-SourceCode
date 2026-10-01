using System;
using System.Collections.Generic;
using UnityEngine;

public class TechRoom1 : MonoBehaviour, ILocation
{
    [SerializeField] string id;
    AreaData locationData;

    [Header("Levichair closeup")]
    [SerializeField] SpriteRenderer chairRenderer;
    [SerializeField] List<Sprite> chairCloseups;
    [SerializeField] Transform chairPosition;

    [Space]
    [SerializeField] GameObject cameras;

    [Header("Roof")]
    [SerializeField] Animator roofAnimator;
    [SerializeField] string roofWorldState;

    [Space]
    [SerializeField] WalkTrigger coldComment;
    [SerializeField] string coldCommentState;
    [SerializeField] AudioEmitter ambience;


    GameManager gameMan;

    private void Start()
    {
        gameMan = GameManager.Instance;

    }

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

    public void SetChairCoseup(int index)
    {
        chairRenderer.sprite = chairCloseups[index];
    }

    public void SwitchToCloseup(bool closeup)
    {
        gameMan.closeupState = closeup;

        if (closeup)
        {
            cameras.transform.position = chairPosition.position;
        }
    }

    public void SwitchRoof()
    {
        print("Switching roof");
        bool roofOpen = gameMan.GetWorldState(roofWorldState);
        //gameMan.AddWorldstate(roofWorldState, !roofOpen);
        SetRoof(roofOpen);
    }

    public void SetRoof(bool roofOpen)
    {
        print("Setting roof");

        if (roofOpen)
        {
            roofAnimator.SetTrigger("Off");
            gameMan.SetWorldstate(roofWorldState, false);
        }
        else
        {
            roofAnimator.SetTrigger("On");
            gameMan.SetWorldstate(roofWorldState, true);
        }
    }

    public void ApplyState()
    {
        gameMan = GameManager.Instance;

        if (!gameMan.GetWorldState(coldCommentState))
        {
            coldComment.Trigger();
            gameMan.SetWorldstate(coldCommentState, true);
        }

        SetRoof(gameMan.GetWorldState(roofWorldState));
    }

    public void TriggerStoryActions(List<string> actions)
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        ambience.Play();
    }

    private void OnDisable()
    {
        ambience.Stop(); 
    }
}
