using UnityEngine;

public class InfoText : MonoBehaviour
{
    public string textKey;

    void ToggleHint()
    {
        if (GetComponent<Interaction>() == null)
            UIManager.Instance.hintsHandler.SpawnHint(transform.position, HintUI.HintColor.Blue);
    }

    private void OnEnable()
    {
        GameEvents.OnToggleHints += ToggleHint;
    }

    private void OnDisable()
    {
        GameEvents.OnToggleHints -= ToggleHint;
    }
}
