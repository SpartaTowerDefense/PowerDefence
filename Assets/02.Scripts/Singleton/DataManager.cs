using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
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

        Debug.Log(savePath);
    }
    public SaveData Load()
    {
        if (!File.Exists(savePath))
        {
            return null;
        }
        string Json = File.ReadAllText(savePath);
        return JsonUtility.FromJson<SaveData>(Json);
    }
}
