using System.IO;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private string savePath => Application.persistentDataPath +"/save.txt"; //저장하는 파일경로

    public void Save(Commander commander, int stage) //저장하는 메서드
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
        if (!File.Exists(savePath))  //저장된 파일이 없다면
        {
            return null;
        }
        string Json = File.ReadAllText(savePath); 
        return JsonUtility.FromJson<SaveData>(Json);
    }
}
