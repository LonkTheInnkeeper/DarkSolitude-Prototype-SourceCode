using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    static IDataService dataService = new JsonDataService();

    static bool debugLog = true;
    static bool debugError = true;

    static string basePath = "GameData/Player/";
    static string infoTextPath = "GameData/InfoText/";

    static string gameSettingsPath = basePath + "SettingsData.json";
    static string playerDataPath = basePath + "PlayerData.json";
    static string inventoryPath = basePath + "InventoryData.json";

    static int saveSlots = 5;

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

        if (dataService.SaveData(basePath + "PlayerData" + playerData.index.ToString() + ".json", playerData))
        {
            MakeScreenshot("PlayerData" +  playerData.index);
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

    // === DELETE ===

    public static bool DeletePlayerData(PlayerData playerData, Sprite screenshot)
    {
        DebugLog("Deleting Player Data");

        if (dataService.DeleteData(basePath + "PlayerData" + playerData.index.ToString() + ".json", playerData))
        {
            dataService.DeleteData(basePath + "PlayerData" + playerData.index.ToString() + ".png", screenshot);
            return true;
        }
        else
        {
            DebugError("Could not delete Player Data");
            return false;
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

    public static PlayerData LoadPlayerData(int index)
    {
        string path = basePath + "PlayerData" + index.ToString() + ".json";

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

    public static List<PlayerData> GetAllPlayerData()
    {
        List<PlayerData> playerDataList = new List<PlayerData>();

        for (int i = 0; i < saveSlots; i++)
        {
            PlayerData playerData = LoadPlayerData(i);

            if (playerData == null)
                continue;
            else
                playerDataList.Add(playerData);
        }

        return playerDataList;
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

    public static InfoTextData LoadInfoText(string localisation)
    {
        string path = infoTextPath + localisation + ".json";

        DebugLog("Loading InfoText");

        if (File.Exists(Application.dataPath + "/" + path))
        {
            InfoTextData infoText = new InfoTextData();
            infoText = dataService.LoadData<InfoTextData>(path);
            return infoText;
        }
        else
        {
            DebugLog("File InfoText does not exist");
            return null;
        }
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

    public static void MakeScreenshot(string name)
    {
        Camera captureCam = Camera.main;

        // Image size
        int width = 854;
        int height = 480;

        // Create folder
        string folderPath = Application.dataPath + "/" + basePath;

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
            Debug.Log("Creating folder");
        }

        // Create RenderTexture
        RenderTexture rt = new RenderTexture(width, height, 24);
        captureCam.targetTexture = rt;

        // Render
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGBA32, false);
        captureCam.Render();
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        // Save PNG
        byte[] bytes = tex.EncodeToPNG();
        string fullPath = Path.Combine(folderPath, name + ".png");
        File.WriteAllBytes(fullPath, bytes);

        // Reset
        captureCam.targetTexture = null;
        RenderTexture.active = null;

        Debug.Log("Screenshot " + name + " saved");
    }

    public static Sprite LoadScreenshot(string name)
    {
        string filePath = Application.dataPath + "/" + basePath + name + ".png";

        if (!File.Exists(filePath)) 
        {
            Debug.LogWarning("File " + name + " does not exist");
            return null;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(fileData);

        Sprite sprite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f),
            100f);

        return sprite;
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
