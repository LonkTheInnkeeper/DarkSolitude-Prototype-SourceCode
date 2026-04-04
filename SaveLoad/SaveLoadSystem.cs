using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveLoadSystem
{
    static IDataService dataService = new JsonDataService();

    static bool debugLog = true;
    static bool debugError = true;

    static string basePath = "GameData/";
    static string infoTextPath = "GameData/InfoText/";

    static string gameSettingsPath = basePath + "SettingsData.json";
    static string GameDataPath = basePath + "GameData.json";
    static string inventoryPath = basePath + "InventoryData.json";

    static int saveSlots = 5;

    // === SAVE ===

    public static void SaveSettingsData(SettingsData settingsData)
    {
        SaveData<SettingsData>("GameSettings.json", settingsData);
    }

    public static void SavePlayerData(GameData gameData)
    {
        if (SaveData<GameData>("GameData" + gameData.index.ToString() + ".json", gameData))
            MakeScreenshot("GameData" + gameData.index);
    }

    // === DELETE ===

    public static bool DeleteGameData(GameData gameData, Sprite screenshot)
    {
        DebugLog("Deleting Game Data");

        if (dataService.DeleteData(basePath + "GameData" + gameData.index.ToString() + ".json", gameData))
        {
            dataService.DeleteData(basePath + "GameData" + gameData.index.ToString() + ".png", screenshot);
            return true;
        }
        else
        {
            DebugError("Could not delete Game Data");
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
        }
        ;
    }

    public static GameData LoadGameData(int index)
    {
        string fileName = "GameData" + index.ToString() + ".json";
        return LoadData<GameData>(fileName);
    }

    public static List<GameData> GetAllGameData()
    {
        List<GameData> gameDataList = new List<GameData>();

        for (int i = 0; i < saveSlots; i++)
        {
            GameData gameData = LoadGameData(i);

            if (gameData == null)
                continue;
            else
                gameDataList.Add(gameData);
        }

        return gameDataList;
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
        }
        ;
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
        return dataService.CheckData(GameDataPath);
    }

    public static bool CheckInventory()
    {
        return dataService.CheckData(inventoryPath);
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

    static bool SaveData<T>(string fileName, T data)
    {
        Debug.Log("Saving data: " + fileName);

        if (dataService.SaveData<T>(basePath + fileName, data))
        {
            Debug.Log("Data saved");
            return true;
        }
        else
        {
            Debug.LogError("Could not save file.");
            return false;
        }
    }

    static T LoadData<T>(string fileName)
    {
        Debug.Log("Loading Data: " + fileName);

        string path = basePath + fileName;
        T data = dataService.LoadData<T>(path);

        if (data == null)
        {
            Debug.LogError("Could not load data");
            return default;
        }

        return data;
    }
}
