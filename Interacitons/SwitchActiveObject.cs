using UnityEngine;

public class SwitchActiveObject : MonoBehaviour
{
    [SerializeField] GameObject gameObject_;
    [SerializeField] bool defaultState;

    bool currentState;

    private void Start()
    {
        currentState = defaultState;
        gameObject_.SetActive(defaultState);
    }

    public void SwitchState()
    {
        currentState = !currentState;
        gameObject_.SetActive(currentState);
    }
}
