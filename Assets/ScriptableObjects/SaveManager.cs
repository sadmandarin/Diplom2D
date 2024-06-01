using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveManager
{
    private static string savePath;

    static Vector3 updatedPos;

    static Quaternion updatedRot;

    static SaveManager()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
    }

    public static void SaveGame(Data gameData)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(savePath, json);
    }

    public static void UpdatePosition(Vector3 pos)
    {
        updatedPos = pos;
    }

    public static void UpdateRotation(Quaternion rot)
    {
        updatedRot = rot;
    }

    public static Vector3 GetPosition() 
    {
        return updatedPos;
    }

    public static Quaternion GetRotation()
    {
        return updatedRot;
    }

    public static Data LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<Data>(json);
        }
        else
        {
            Debug.LogWarning("Save file not found.");
            return null;
        }
    }
}
