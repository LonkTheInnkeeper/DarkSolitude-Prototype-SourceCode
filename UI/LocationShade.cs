using UnityEngine;

public class LocationShade : MonoBehaviour
{
    Animator animator;
    Doors currentDoors;
    GameData gameData;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerShade(Doors doors)
    {
        currentDoors = doors;
        animator.SetTrigger("Location");
    }

    public void SwitchLocation()
    {
        LocationManager.Instance.SwitchLocation(currentDoors);
    }

    public void StartLoadig(GameData gameData) 
    {
        this.gameData = gameData;
        animator.SetTrigger("LoadGame");
    }

    public void LoadGame()
    {
        SaveLoadManager.Instance.LoadGameData(gameData);
    }
}
