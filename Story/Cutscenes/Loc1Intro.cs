using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Loc1Intro : MonoBehaviour, ILocation
{
    [SerializeField] string id;
    public bool playIntroCutscene;
    [Space]
    public Movement profezoro;
    public Doors doors1;
    public SpriteRenderer doors1Renderer;
    public Interaction interaction1;
    public Interaction interaction2;
    public Interaction interaction3;
    public CameraControl camControl;
    [Space]
    public Transform point1;
    public Transform point2;
    [Space]
    public Animator terminal2Anim;
    public Animator terminal2OccAnim;
    public ScreenShade screenShadeAction;

    public UnityEvent endSceneEvent;
    public UnityEvent dialogueEvent;
    public UnityEvent startSceneEvent;

    private void Start()
    {
        if (!playIntroCutscene)
        {
            endSceneEvent.Invoke();
            return;
        }

        StartCoroutine(Phase1());
    }

    IEnumerator Phase1()
    {
        startSceneEvent.Invoke();
        GameManager.Instance.SetGameState(GameManager.GameState.StoryEvent);
        yield return new WaitForSeconds(0.1f);
        camControl.FocusOnTarget(point1);

        doors1.ToggleDoorsAnimation(true);
        profezoro.SetInteractable(interaction1.GetComponent<IInteractable>());
        profezoro.SetPlayerState(Movement.PlayerState.Walking);

        yield return new WaitForSeconds(5);

        doors1.ToggleDoorsAnimation(false);
    }

    public void Phase2()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.StoryEvent);
        profezoro.SetInteractable(interaction2.GetComponent<IInteractable>());
        profezoro.SetPlayerState(Movement.PlayerState.Walking);
    }

    public void Phase3()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.StoryEvent);
        profezoro.SetInteractable(interaction3.GetComponent<IInteractable>());
        profezoro.SetPlayerState(Movement.PlayerState.Walking);

        StartCoroutine(Phase3Doors());
    }

    public void StartGame()
    {
        screenShadeAction.TriggerShade(endSceneEvent, ScreenShade.ShadeType.ShadeOff);
        print("Starting game");
    }

    IEnumerator Phase3Doors()
    {
        yield return new WaitForSeconds(2);
        doors1.ToggleDoorsAnimation(true);

        yield return new WaitForSeconds(1.5f);

        yield return new WaitForSeconds(1.5f);
        doors1.ToggleDoorsAnimation(false);

        yield return new WaitForSeconds(1.5f);
        screenShadeAction.TriggerShade(dialogueEvent, ScreenShade.ShadeType.ShadeOn);
    }

    public void EndScene()
    {
    }

    public string GetID()
    {
        return id;
    }

    public void ApplyState()
    {
        throw new System.NotImplementedException();
    }

    public void TriggerStoryActions(List<string> actions)
    {
        foreach (string action in actions)
        {
            print("Triggering action: " + action);

            switch (action)
            {
                case "Phase2":
                    Phase2(); continue;
                case "Phase3":
                    Phase3(); continue;
                case "StartGame":
                    StartGame(); continue;
                case "EndScene":
                    EndScene(); continue;
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
