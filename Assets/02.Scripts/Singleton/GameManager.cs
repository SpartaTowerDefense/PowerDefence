using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameObject> stageMaps;

    private int currentStage = 0;

    public Commander commander { get; private set; } = new(20, 5000);

    private void Start()
    {
        //SaveGame();
        LoadGame();
    }
    public void StageClear()
    {
        currentStage++;
        ActiveStage(currentStage);

        SaveGame();
    }
    private void ActiveStage(int stage)
    {
        for (int i = 0; i < stageMaps.Count; i++)
        {
            stageMaps[i].SetActive(i == stage); //i번째가 해당 stage와 일치하는 것만 SetActive시키기
        }
    }

    public void SaveGame()
    {
        DataManager.Instance.Save(commander,currentStage);
    }
    public void LoadGame()
    {
        SaveData data = DataManager.Instance.Load();
        if(data != null)
        {
            commander = new Commander(20, data.gold); // 체력은 항상 20
            currentStage = data.stage;

            ActiveStage(currentStage);
        }
    }
}
