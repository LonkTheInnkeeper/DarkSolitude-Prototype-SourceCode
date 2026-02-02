using System.Collections;
using UnityEngine;

public class LocationSwitch : MonoBehaviour
{
    [SerializeField] int currentLocationIndex;
    [SerializeField] int targetLocationIndex;

    [Space]
    [SerializeField] GameObject currentLocation;
    [SerializeField] GameObject targetLocation;

    [Space]
    [SerializeField] Doors targetDoors;

    Doors currentDoors;

    public void StartSwitch()
    {
        currentDoors = GetComponent<Doors>();
        if (!currentDoors.ToggleDoors(true)) return;

        UIManager.Instance.locationShade.StartSwitch(this);
    }

    public void SwitchLocation()
    {
        Movement playerMovement = GameManager.Instance.player.GetComponent<Movement>();

        //LocationManager.instance.ToggleLocation(true, targetLocationIndex);
        targetLocation.SetActive(true);

        playerMovement.WarpTo(targetDoors.spawnPoint.position);
        playerMovement.SetDestination(targetDoors.spawnPoint.position);
        playerMovement.transform.rotation = targetDoors.spawnPoint.rotation;

        targetDoors.ToggleDoors(false);
        //LocationManager.instance.ToggleLocation(false, currentLocationIndex);
        currentLocation.SetActive(false);
    }
}
