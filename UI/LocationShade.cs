using UnityEngine;

public class LocationShade : MonoBehaviour
{
    Animator animator;
    LocationSwitch locationSwitch;
    PlayerData playerData;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartSwitch(LocationSwitch locationSwitch)
    {
        this.locationSwitch = locationSwitch;
        animator.SetTrigger("Location");
    }

    public void SwitchLocation()
    {
        locationSwitch.SwitchLocation();
    }

    public void StartLoadig(PlayerData playerData) 
    {
        this.playerData = playerData;
        animator.SetTrigger("LoadGame");
    }

    public void LoadGame()
    {
        SaveLoadManager.Instance.LoadPlayerData(playerData);
    }
}
