using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : Singleton<AudioManager>
{
    private string savePath => Application.persistentDataPath +"/save.txt";

    public void Save(Commander commander, int stage)
    {
        SaveData saveData = new SaveData
        {
            gold = commander.gold,
            stage = stage

        };
        string Json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, Json);

    }

  
}
