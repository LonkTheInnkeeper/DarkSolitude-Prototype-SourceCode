using System;
using System.Collections.Generic;
using UnityEngine;

public class Loc1 : MonoBehaviour, ILocation
{
    [SerializeField] string id;

    [SerializeField] Doors doors1;
    [SerializeField] Doors doors3;
    [SerializeField] string doors1OnState;
    [SerializeField] string doors3UnlockState;

    [Space]
    [SerializeField] string introComment;
    [SerializeField] bool startDialogueState;

    [Space]
    [SerializeField] TutorialUI tutorial;

    private void Start()
    {
        ApplyState();
        GameManager.Instance.AddWorldstate("new_game", false);
        GameManager.Instance.gameData.areaData.currentLocationID = id;

        if (!GameManager.Instance.CheckWorldState(introComment) && startDialogueState)
        {
            StoryManager.Instance.StartComment(introComment);
            GameManager.Instance.AddWorldstate(introComment, true);
        }
    }

    public void ApplyState()
    {
        GameManager gameMan = GameManager.Instance;

        if (gameMan.gameData.areaData.worldStates.ContainsKey(doors1OnState))
            doors1.SetDoorsOn(gameMan.CheckWorldState(doors1OnState));

        doors3.SetDoorUnlocked(gameMan.CheckWorldState(doors3UnlockState));
    }

    public string GetID()
    {
        return id;
    }

    private void OnEnable()
    {

    }

    public void TriggerStoryActions(List<string> actions)
    {
        foreach (string action in actions)
        {
            print("Triggering action: " + action);

            switch (action)
            {
                case "Tutorial":
                    {
                        print("Tutorial switch");
                        tutorial.ToggleTutorial(); break;
                    }
                default:
                    print("No action"); break;
            }
        }
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
