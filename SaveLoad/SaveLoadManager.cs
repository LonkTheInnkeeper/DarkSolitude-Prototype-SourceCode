using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;

    GameManager gameMan;
    InventoryManager inventoryMan;
    LocationManager locationMan;

    public SaveLoadSlot sellectedSaveSlot;

    List<ISavable> savables = new List<ISavable>();

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

        savables = FindObjectsByType<MonoBehaviour>().OfType<ISavable>().ToList();
    }

    private void Start()
    {
        gameMan = GameManager.Instance;
        inventoryMan = InventoryManager.Instance;
        locationMan = LocationManager.Instance;

        //LoadPlayerData(SaveLoadSystem.LoadPlayerData(2));
    }

    public void SaveGameData(string fileName)
    {
        GameData gameData = gameMan.gameData;

        gameData.index = sellectedSaveSlot.index;
        gameData.UpdateFileDate();
        gameData.fileName = fileName;
        gameMan.player.GetComponent<Player>().SavePosition(gameData);
        //inventoryMan.inventory.SaveInventory(playerData);

        SaveLoadSystem.SavePlayerData(gameData);
    }

    public void SaveSettings(SettingsData settingsData)
    {
        SaveLoadSystem.SaveSettingsData(settingsData);
    }

    public void LoadGameData(GameData gameData)
    {
        if (gameData == null) return;

        gameMan.gameData = gameData;
        gameMan.player.GetComponent<Player>().LoadPosition(gameData);
        locationMan.SwitchLocation(gameData.areaData.currentLocationID);

        foreach (var savable in savables)
        {
            savable.ApplyState();
        }
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

    public void DeteleFile()
    {
        SaveLoadSystem.DeleteGameData(sellectedSaveSlot.gameData, null);
    }
}
