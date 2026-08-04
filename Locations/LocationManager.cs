using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager Instance;

    [SerializeField] List<GameObject> locations;

    [Space]
    [SerializeField] GameObject cameras;

    GameManager gameMan;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameMan = GameManager.Instance;

        foreach (var location in locations)
        {
            if (location.gameObject.activeInHierarchy)
            {
                gameMan.gameData.areaData.currentLocationID = location.GetComponent<ILocation>().GetID();
                return;
            }
        }
    }

    public void SwitchLocation(string id)
    {
        foreach (var location in locations)
        {
            ILocation iLocation = location.GetComponent<ILocation>();
            bool isTarget = iLocation.GetID() == id;

            if (isTarget)
                iLocation.ApplyState();

            location.SetActive(isTarget);
        }

        gameMan.gameData.areaData.currentLocationID = id;
        gameMan.cameraControl.FocusOnPlayer();
        //GameEvents.OnLocationLoad?.Invoke();
    }

    public void SwitchLocation(Doors doors)
    {
        Movement playerMovement = gameMan.player.GetComponent<Movement>();

        string targetLocationId = doors.targetLocation.GetComponent<ILocation>().GetID();
        gameMan.gameData.areaData.currentLocationID = targetLocationId;
        SwitchLocation(targetLocationId);

        playerMovement.WarpTo(doors.targetDoors.spawnPoint.position);
        playerMovement.SetDestination(doors.targetDoors.spawnPoint.position);
        playerMovement.transform.rotation = doors.targetDoors.spawnPoint.rotation;

        doors.targetDoors.ToggleDoorsAnimation(false);

        GameEvents.OnLocationLoad?.Invoke();
    }

    public void ToggleLocation(bool toggle, string id)
    {
        print("Toggling location " + id);

        GameObject location = locations.FirstOrDefault(item => item.GetComponent<ILocation>().GetID() == id);

        if (location != null)
        {
            print("Location " + id + " toggle " + toggle);
            location.SetActive(toggle);
        }
        else
        {
            Debug.LogWarning("Location not found");
        }
    }

    public ILocation GetActiveILocation()
    {
        foreach (var location in locations)
        {
            ILocation iLocation = location.GetComponent<ILocation>();

            if (iLocation.GetID() == gameMan.gameData.areaData.currentLocationID)
                return iLocation;
        }

        return null;
    }

    public void StartCloseup()
    {

    }
}
