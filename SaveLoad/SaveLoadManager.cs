using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    GameManager gameMan;
    InventoryManager inventoryMan;
    LocationManager locationMan;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
        locationMan = LocationManager.instance;

        //LoadPlayerData(SaveLoadSystem.LoadPlayerData(2));
    }

    public void SavePlayerData(int index, string fileName)
    {
        PlayerData playerData = gameMan.playerData;

        playerData.index = index;
        playerData.UpdateFileDate();
        playerData.fileName = fileName;
        LocationManager.instance.SaveLocationData(playerData);
        gameMan.player.GetComponent<Player>().SavePosition(playerData);
        //inventoryMan.inventory.SaveInventory(playerData);

        SaveLoadSystem.SavePlayerData(playerData);
    }

    public void SaveSettings(SettingsData settingsData)
    {
        SaveLoadSystem.SaveSettingsData(settingsData);
    }

    public void LoadPlayerData(PlayerData playerData)
    {
        if (playerData == null) return;

        gameMan.playerData = playerData;
        gameMan.player.GetComponent<Player>().LoadPosition(playerData);
        locationMan.SetLoactionData(playerData.locations);
        locationMan.SwitchLocation(playerData.currentLocation);

        //inventoryMan.inventory.LoadInventory();
    }

    public SettingsData LoadSettings()
    {
        SettingsData settingsData = new SettingsData();

        if (SaveLoadSystem.CheckSettingsData())
        {
            settingsData = SaveLoadSystem.LoadSettingsData();
        }
        else
        {
            SaveLoadSystem.SaveSettingsData(settingsData);
        }

        return settingsData;
    }
}
