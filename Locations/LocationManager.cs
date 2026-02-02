using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocationManager : MonoBehaviour
{
    public static LocationManager instance;

    [SerializeField] List<GameObject> locations;
    List<ILocation> iLocations;

    public bool debugLocation;
    public int debugLocationIndex;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        iLocations = GrabInterfaces();

    }

    List<ILocation> GrabInterfaces()
    {
        List<ILocation> interfaces = new List<ILocation>();

        foreach (var gObject in locations)
        {
            interfaces.Add(gObject.GetComponent<ILocation>());
        }

        return interfaces;
    }

    public void SetLoactionData(List<LocationData> data)
    {
        foreach (var iLocation in iLocations)
        {
            LocationData locationData = data.FirstOrDefault(item => item.index == iLocation.GetIndex());

            if (locationData != null)
                iLocation.SetData(locationData);
            else
                iLocation.SetData(new LocationData(iLocation.GetIndex()));
        }
    }

    public ILocation GetLocation(int index)
    {
        return iLocations.FirstOrDefault(item => item.GetIndex() == index);
    }

    public void SwitchLocation(int index)
    {
        foreach (var location in locations)
        {
            if (location.activeInHierarchy)
            {
                location.SetActive(false);

            }

            if (location.GetComponent<ILocation>().GetIndex() == index)
            {
                location.SetActive(true);
                GameManager.Instance.playerData.currentLocation = index;
            }
        }
    }

    public void ToggleLocation(bool toggle, int index)
    {
        print("Toggling location " + index);

        GameObject location = locations.FirstOrDefault(item => item.GetComponent<ILocation>().GetIndex() == index);

        if (location != null)
        {
            print("Location " + index + " toggle " + toggle);
            location.SetActive(toggle);
        }
        else
        {
            Debug.LogWarning("Location not found");
        }
    }

    public void SaveLocationData(PlayerData playerData)
    {
        List<LocationData> data = new List<LocationData>();

        foreach (var location in iLocations)
        {
            data.Add(location.GetData());
        }

        playerData.SaveLocations(data);
    }

    public void SaveEventKey(int index, string key)
    {
        foreach (var iLocation in iLocations)
        {
            if (iLocation.GetIndex() == index)
            {
                iLocation.AddKey(key);
                break;
            }
        }
    }
}
