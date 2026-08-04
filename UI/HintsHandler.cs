using System.Collections;
using UnityEngine;

public class HintsHandler : MonoBehaviour
{
    [SerializeField] GameObject hintPref;
    [SerializeField] Transform hintsParent;
    [SerializeField] float refreshTime;
    float hintLife;

    bool hinted = false;

    public void SpawnHint(Vector3 position, HintUI.HintColor color)
    {
        if (hinted) return;

        var hint = Instantiate(hintPref, hintsParent);
        hint.GetComponent<HintUI>().Init(position, color, refreshTime);

        StartCoroutine(Refresh());
    }

    IEnumerator Refresh()
    {
        yield return new WaitForEndOfFrame();
        hinted = true;
        yield return new WaitForSeconds(refreshTime);
        hinted = false;
    }
}
