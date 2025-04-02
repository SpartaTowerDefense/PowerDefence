using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameObject> stageMaps;
    private List<GameObject> stageMapInstances = new();
    private List<Placement> placements = new();
    private List<EnemySpawner> enemySpawners = new();

    private EnemySpawner enemySpawner;
    public EnemySpawner EnemySpawner => enemySpawner;

    private int currentStage = 0;
    public int CurrentStage => currentStage;
    public Commander commander { get; private set; } = new(20, 5000);

    protected override void Awake()
    {
        base.Awake();
        AudioManager.Instance.Initinalize();
        CreateStages();
    }

    private void Start()
    {
        UIManager.Instance.EndPanel.GameEnd();
        SaveGame();
        //LoadGame();
    }

    public void StageClear()
    {
        if (currentStage < stageMaps.Count)
        {
            currentStage++;
            ActiveStage(currentStage);
            SaveGame();
        }
        else
        {
            UIManager.Instance.EndPanel.GameEnd();
        }
        
    }
    private void ActiveStage(int stage)
    {
        for (int i = 0; i < stageMaps.Count; i++)
        {
            stageMaps[i].SetActive(i == stage); //i번째가 해당 stage와 일치하는 것만 SetActive시키기
        }
        Placement placement = FindObjectOfType<Placement>();
        placements[stage].SetStageIndex(stage); // 타일맵 인덱스 갱신

        ((EnemyFactory)FactoryManager.Instance.path[nameof(EnemyFactory)]).SetPathByStage(stage);
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    public void SaveGame()
    {
        DataManager.Instance.Save(commander, currentStage);
    }
    public void LoadGame()
    {
        SaveData data = DataManager.Instance.Load();
        if (data != null)
        {
            commander = new Commander(20, data.gold); // 체력은 항상 20으로 고정
            currentStage = data.stage;

            ActiveStage(currentStage);
        }
    }
    public void CreateStages()
    {
        GameObject[] stagePrefabs = Resources.LoadAll<GameObject>("stageMaps");
        
        for(int i = 0; i < stagePrefabs.Length; i++)
        {
            GameObject maps = Instantiate(stagePrefabs[i]);
            maps.name = $"stageMap_{i}";
            maps.SetActive(false);
            stageMapInstances.Add(maps);
            
            if(placements.Count == 0)
            {
                Placement placement = UIManager.Instance.GetComponentInChildren<Placement>(true);

                if(placement != null)
                {
                    placements.Add(placement);
                }
            }
        }  
     }
}
