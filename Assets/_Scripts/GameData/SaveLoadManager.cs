using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager {

    private static string _fileName = "/save.txt";

    public static void SaveData(PlayerData data)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + _fileName, FileMode.Create);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadData()
    {
        if (File.Exists(Application.persistentDataPath + _fileName))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + _fileName, FileMode.Open);

            PlayerData playerData = bf.Deserialize(stream) as PlayerData;
            stream.Close();

            return playerData;
        }
        Debug.Log("File not found");
        PlayerData data = new PlayerData();
        data.LevelResults = new List<LevelResult>();
        return data;
    }
    public static void RemoveData()
    {
        File.Delete(Application.persistentDataPath + _fileName);
        //FileStream stream = new FileStream(Application.dataPath + _fileName, FileMode.R);
    }
}
