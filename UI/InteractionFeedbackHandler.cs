using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class InteractionFeedbackHandler : MonoBehaviour
{
    [SerializeField] GameObject feedbackPref;
    [SerializeField] Transform hintsParent;
    [SerializeField] int refreshTime;
    float hintLife;

    Vector3 position;

    bool hinted = false;
    GameObject feedback;

    void LateUpdate()
    {
        if (feedback == null) return;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        feedback.GetComponent<RectTransform>().position = screenPos;
    }

    public void SpawnFeedback(Vector3 position)
    {
        if (hinted) return;

        this.position = position;

        feedback = Instantiate(feedbackPref, hintsParent);
        hinted = true;

        StartCoroutine(Refresh());
    }

    IEnumerator Refresh()
    {
        yield return new WaitForSeconds(refreshTime);
        hinted = false;
        Destroy(feedback);
    }
}
