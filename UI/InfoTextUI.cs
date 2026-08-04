using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] Image mask;
    [SerializeField] RectTransform rect;
    [SerializeField] float rectScale;
    [SerializeField] float animationSpeed;

    bool textUp;

    public void ToggleInfotext(bool toggle, string key)
    {
        if (GameManager.Instance.GetGameState() != GameManager.GameState.Navigation) return;

        RectAnimation(toggle);

        if (key != string.Empty)
            textUI.text = StoryManager.Instance.storyDatabase.GetInfoText(key);
    }

    void RectAnimation(bool toggle)
    {
        if (toggle && !textUp)
        {
            StartCoroutine(UITools.ScaleRectRoutine(rect, new Vector3(rectScale, rectScale), animationSpeed));
            StartCoroutine(UITools.AlphaShadeRoutine(mask, 1, animationSpeed));
        }
        else if (!toggle && textUp)
        {
            StartCoroutine(UITools.ScaleRectRoutine(rect, new Vector3(rectScale / 1.5f, rectScale / 1.5f), animationSpeed));
            StartCoroutine(UITools.AlphaShadeRoutine(mask, 0, animationSpeed));
        }

        textUp = toggle;
    }

    void CloseUIText()
    {
        if (textUp)
            RectAnimation(false);
    }

    private void OnEnable()
    {
        GameEvents.OnGameStateChange += CloseUIText;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStateChange -= CloseUIText;
    }
}