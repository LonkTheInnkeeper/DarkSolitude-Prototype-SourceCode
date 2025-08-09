using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    static IDataService dataService = new JsonDataService();

    static bool debugLog = true;
    static bool debugError = true;

    static string basePath = "GameData/Player/";

    static string gameSettingsPath = basePath + "SettingsData.json";
    static string playerDataPath = basePath + "PlayerData.json";
    static string inventoryPath = basePath + "InventoryData.json";

    // === SAVE ===

    public static void SaveSettingsData(SettingsData settingsData)
    {
        DebugLog("Saving Game Settings");
        if (dataService.SaveData(gameSettingsPath, settingsData))
        {
            return;
        }
        else
        {
            DebugError("Could not save Game Settings");
        }
    }

    public static void SavePlayerData(PlayerData playerData)
    {
        DebugLog("Saving Player Data");

        if (dataService.SaveData(playerDataPath, playerData))
        {
            return;
        }
        else
        {
            DebugError("Could not save Player Data");
        }
    }

    public static void SaveInventory(InventoryData inventory)
    {
        DebugLog("Saving Invenory");

        if (dataService.SaveData(inventoryPath, inventory))
        {
            return;
        }
        else
        {
            DebugError("Could not save Inventory");
        }
    }


    // === LOAD ===

    public static SettingsData LoadSettingsData()
    {
        string path = gameSettingsPath;

        DebugLog("Loading Game Settings");

        if (File.Exists(Application.dataPath + "/" + path))
        {
            SettingsData gameSettings = new SettingsData();
            gameSettings = dataService.LoadData<SettingsData>(path);
            return gameSettings;
        }
        else
        {
            DebugLog("File Game Settings does not exist");
            return null;
        };
    }

    public static PlayerData LoadPlayerData()
    {
        string path = playerDataPath;

        DebugLog("Loading Player Data");

        if (File.Exists(Application.dataPath + "/" + path))
        {
            PlayerData playerData = new PlayerData();
            playerData = dataService.LoadData<PlayerData>(path);
            return playerData;
        }
        else
        {
            DebugLog("File Player Data does not exist");
            return null;
        };
    }

    public static InventoryData LoadInventory()
    {
        string path = inventoryPath;

        DebugLog("Loading Inventory");

        if (File.Exists(Application.dataPath + "/" + path))
        {
            InventoryData inventory = new InventoryData();
            inventory = dataService.LoadData<InventoryData>(path);
            return inventory;
        }
        else
        {
            DebugLog("File Inventory does not exist");
            return null;
        };
    }


    // === CHECK ===

    public static bool CheckSettingsData()
    {
        return dataService.CheckData(gameSettingsPath);
    }

    public static bool CheckPlayerData()
    {
        return dataService.CheckData(playerDataPath);
    }

    public static bool CheckInventory()
    {
        return dataService.CheckData(inventoryPath);
    }


    // Other saves

    public static List<string> LoadStringList(string path)
    {
        if (File.Exists(Application.dataPath + "/" + path))
        {
            List<string> strings = new List<string>();
            strings = dataService.LoadData<List<string>>(path);
            return strings;
        }
        else
        {
            DebugLog("Null data");
            return null;
        };
    }

    public static void SaveStringList(List<string> list, string path)
    {
        if (dataService.SaveData(path, list))
        {
            return;
        }
        else
        {
            DebugError("Could not save file");
        }
    }

    static void DebugLog(string msg)
    {
        if (debugLog) Debug.Log(msg);
    }

    static void DebugError(string msg)
    {
        if (debugError) Debug.LogError(msg);
    }
}
