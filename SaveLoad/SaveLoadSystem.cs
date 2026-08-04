using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveLoadSystem
{
    static IDataService dataService = new JsonDataService();

    static bool debugLog = true;
    static bool debugError = true;

    static string basePath = "GameData/";
    static string infoTextPath = "Resources/Story/Infotext/";

    static string gameSettingsPath = basePath + "GameSettings.json";
    static string GameDataPath = basePath + "GameData.json";
    static string inventoryPath = basePath + "InventoryData.json";

    static int saveSlots = 3;

    // === SAVE ===

    public static void SaveSettingsData(SettingsData settingsData)
    {
        string path = Path.Combine(Application.persistentDataPath, "settings.json");
        string json = JsonConvert.SerializeObject(settingsData, Formatting.Indented);

        try
        {
            if (File.Exists(path))
            {
                DebugLog("Data exists. Deleting old file and creating a new one.");
                File.Delete(path);
            }
            else
            {
                DebugLog("Creating a new file");
            }

            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(settingsData));
        }

        catch (Exception e)
        {
            DebugError($"Unable to save data due to: {e.Message} {e.StackTrace}");
        }


        //File.WriteAllText(path, json);

        //SaveData<SettingsData>("GameSettings.json", settingsData);
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
        string path = Path.Combine(Application.persistentDataPath, "settings.json");

        DebugLog("Loading Game Settings");

        if (File.Exists(path))
        {
            SettingsData gameSettings = new SettingsData();
            gameSettings = JsonConvert.DeserializeObject<SettingsData>(File.ReadAllText(path));
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
        string path = Path.Combine(Application.streamingAssetsPath, "InfoTexts", "InfoText_" + localisation + ".json");

        if (File.Exists(path))
        {
            InfoTextData infoText = new InfoTextData();
            infoText = JsonConvert.DeserializeObject<InfoTextData>(File.ReadAllText(path));
            return infoText;
        }
        else
        {
            DebugLog("File InfoText does not exist");
            return null;
        }
    }

    public static UITextData LoadUITextData(SettingsData.TextLang localisation)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "UITexts", "UI_" + localisation + ".json");

        if (File.Exists(path))
        {
            UITextData uiText = new UITextData();
            uiText = JsonConvert.DeserializeObject<UITextData>(File.ReadAllText(path));
            return uiText;
        }
        else
        {
            DebugLog("File UIText does not exist");
            return null;
        }
    }

    public static UITextData LoadUITextData(SettingsData.VoiceLang localisation)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "UITexts", "UI_" + localisation + ".json");

        if (File.Exists(path))
        {
            UITextData uiText = new UITextData();
            uiText = JsonConvert.DeserializeObject<UITextData>(File.ReadAllText(path));
            return uiText;
        }
        else
        {
            DebugLog("File UIText does not exist");
            return null;
        }
    }

    public static CreditsData LoadEndCreditsData(SettingsData.TextLang localisation)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "UITexts", "CreditsData_" + localisation + ".json");

        if (File.Exists(path))
        {
            CreditsData creditsData = new CreditsData();
            creditsData = JsonConvert.DeserializeObject<CreditsData>(File.ReadAllText(path));
            return creditsData;
        }
        else
        {
            DebugLog("File CreditsData does not exist");
            return null;
        }
    }

    public static StoryData LoadStoryData(string name, string localisation)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Story", "Dialogues", name, name + "_" + localisation + ".json");

        if (File.Exists(path))
        {
            StoryData storyData = new StoryData();
            storyData = JsonConvert.DeserializeObject<StoryData>(File.ReadAllText(path));
            return storyData;
        }
        else
        {
            Debug.LogWarning($"Story {name}.{localisation} does not exist");
            Debug.LogWarning(path);
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
            return default;
        }

        return data;
    }
}
