using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class JsonDataService : IDataService
{
    private static bool debugLog = true;
    private static bool debugError = true;

    public bool SaveData<T>(string relativePath, T data)
    {
        string path = Application.dataPath + "/" + relativePath;

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
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
            return true;
        }

        catch (Exception e)
        {
            DebugError($"Unable to save data due to: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string relativePath)
    {
        string path = Application.dataPath + "/" + relativePath;

        if (!File.Exists(path))
        {
            DebugLog("Cannot load data. The file doesn't exist");
            throw new Exception($"{path} file doesn't exist");
        }

        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (Exception e)
        {
            DebugError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }

    public bool CheckData(string relativePath)
    {

        string path = Application.dataPath + "/" + relativePath;

        if (File.Exists(path)) return true;
        else return false;
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
