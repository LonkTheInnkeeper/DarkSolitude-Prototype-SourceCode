using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UICursorDistanceTrigger : MonoBehaviour
{
    [SerializeField] private DistanceMode distanceMode = DistanceMode.XY;
    [SerializeField] float maxDistance = 100f;
    [SerializeField] float minDistance = 100f;
    [SerializeField] bool applyActiveGameStates = true;
    [SerializeField] List<GameManager.GameState> activeGameStates;

    [SerializeField] UnityEvent maxDistanceTrigger;
    [SerializeField] UnityEvent minDistanceTrigger;

    RectTransform targetUI;
    bool isInside;

    public enum DistanceMode
    {
        XY,
        X,
        Y
    }

    private void Start()
    {
        targetUI = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!GameStateCheck()) return;

        Vector2 mousePos = new Vector2(Input.mousePosition.x / Screen.width,
                                       Input.mousePosition.y / Screen.height);

        Vector2 uiPos = new Vector2(targetUI.position.x / Screen.width,
                                    targetUI.position.y / Screen.height);

        float distance = GetDistance(mousePos, uiPos) * 100;

        if (distance > minDistance && distance < maxDistance) return;

        if (distance > maxDistance && isInside)
        {
            MaxDistance();
        }

        if (distance < minDistance && !isInside)
        {
            MinDistance();
        }
    }

    void MinDistance()
    {
        isInside = true;
        minDistanceTrigger.Invoke();
    }

    void MaxDistance()
    {
        isInside = false;
        maxDistanceTrigger.Invoke();
    }

    private float GetDistance(Vector2 mousePos, Vector2 uiPos)
    {
        switch (distanceMode)
        {
            case DistanceMode.X:
                return Mathf.Abs(mousePos.x - uiPos.x);
                 
            case DistanceMode.Y:
                return Mathf.Abs(mousePos.y - uiPos.y);

            default:
                return Vector2.Distance(mousePos, uiPos);
        }
    }

    private bool GameStateCheck()
    {
        if (!applyActiveGameStates)
            return true;

        GameManager.GameState currentState = GameManager.Instance.GetGameState();

        foreach (GameManager.GameState state in activeGameStates)
        {
            if (state == currentState)
                return true;
        }

        return false;
    }

    public void ResetState()
    {
        isInside = false;
    }
}
