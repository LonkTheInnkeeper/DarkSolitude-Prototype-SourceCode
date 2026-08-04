using System.Collections.Generic;
using UnityEngine;

public class CloseupControl : MonoBehaviour
{
    public string closeupName;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> frames;
    public List<GameObject> interactables;
    public Transform cameraPoint;

    GameManager gameMan;

    private void Start()
    {
        gameMan = GameManager.Instance;
    }

    public void StartCloseup()
    {
        gameMan.SetGameState(GameManager.GameState.Closeup);
        gameMan.closeupState = true;

        SetFrame(gameMan.gameData.areaData.GetCloseupFrame(closeupName));
        cameraPoint.position = spriteRenderer.transform.position;
    }

    public void StopCloseup()
    {
        if (!gameObject.activeInHierarchy) return;

        gameMan.SetGameState(GameManager.GameState.Navigation);
        gameMan.closeupState = false;

        cameraPoint.GetComponent<CameraControl>().FocusOnPlayer();
    }

    public void SetFrame(int index)
    {
        if (frames.Count <= index)
        {
            Debug.LogWarning("Frame index too high");
            return;
        }

        spriteRenderer.sprite = frames[index];

        for (int i = 0; i < interactables.Count; i++)
        {
            if (i == index)
            {
                interactables[i].SetActive(true);
                continue;
            }

            interactables[i].SetActive(false);
        }

        gameMan.gameData.areaData.SetCloseup(closeupName, index);
    }
}
