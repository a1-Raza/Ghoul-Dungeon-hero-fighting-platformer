using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SaveLevels(LevelManager levelManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "LevelData.pdf");
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);

        formatter.Serialize(stream, data);
        stream.Close();

        //Debug.Log("Data Saved");
    }

    public static LevelData LoadLevels()
    {
        string path = Path.Combine(Application.persistentDataPath, "LevelData.pdf");
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LevelData data = formatter.Deserialize(stream) as LevelData;
            stream.Close();

            //Debug.Log("Number of levels unlocked: " + data.totalNumLevelsUnlocked);
            //Debug.Log("Saved to " + path);
            //Debug.Log("Data Loaded | Levels Unlocked: " + data.totalNumLevelsUnlocked);
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }

    public static void ResetLevels(LevelManager levelManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Path.Combine(Application.persistentDataPath, "LevelData.pdf");
        FileStream stream = new FileStream(path, FileMode.Create);

        LevelData data = new LevelData(levelManager);
        
        data.totalNumLevelsUnlocked = 1;

        formatter.Serialize(stream, data);
        stream.Close();

        //Debug.Log("Data Reset");
    }
}
