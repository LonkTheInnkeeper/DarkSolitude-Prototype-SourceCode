using System.Collections;
using TMPro;
using UnityEngine;

public class InfoTextUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] TextMeshProUGUI textUI;
    [SerializeField] GameObject infoObject;

    bool lineMode = false;

    private void Start()
    {
        infoObject.SetActive(false);
    }

    public void ToggleInfotext(bool toggle, int textIndex)
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Navigation || lineMode) return;

        if (!infoObject.activeInHierarchy && toggle)
            infoObject.SetActive(true);

        if (!animator.gameObject.activeInHierarchy)
            animator.gameObject.SetActive(true);

        if (textIndex >= 0)
            textUI.text = DialogueManager.Instance.infoTextDatabase.GetInfoText(textIndex);

        animator.SetBool("Trigger", toggle);
    }

    public IEnumerator ToggleInfotextDelayed(bool toggle, int textIndex, float time)
    {
        if (GameManager.Instance.gameState != GameManager.GameState.Navigation || lineMode) yield return null;

        if (!infoObject.activeInHierarchy && toggle)
            infoObject.SetActive(true);

        if (!animator.gameObject.activeInHierarchy)
            animator.gameObject.SetActive(true);

        if (textIndex >= 0)
            textUI.text = DialogueManager.Instance.infoTextDatabase.GetInfoText(textIndex);

        yield return new WaitForSeconds(time);

        animator.SetBool("Trigger", toggle);
    }

    public IEnumerator ToggleInfoVoicelineDelayed(string text, float time)
    {
        print("Combo line");

        lineMode = true;

        if (!infoObject.activeInHierarchy)
            infoObject.SetActive(true);

        if (!animator.gameObject.activeInHierarchy)
        {
            animator.gameObject.SetActive(true);
        }

        animator.SetBool("Trigger", true);
        textUI.text = text;

        yield return new WaitForSeconds(time);

        animator.SetBool("Trigger", false);
        print("End of the line");
        lineMode = false;
    }

    public void TriggerVoicelineDelayed(string text, float time)
    {
        StartCoroutine(ToggleInfoVoicelineDelayed(text, time));
    }

    public void TriggerVoicelineDelayed(string text)
    {
        StartCoroutine(ToggleInfoVoicelineDelayed(text, 3.5f));
    }
}
