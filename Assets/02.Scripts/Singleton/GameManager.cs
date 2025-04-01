using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameObject> stageMaps;

    private int currentStage = 0;

    public Commander commander { get; private set; } = new(20, 5000);

    protected override void Awake()
    {
        base.Awake();
        AudioManager.Instance.Initinalize();
    }


    public void StageClear()
    {
        currentStage++;
        ActiveStage(currentStage);

        // Json으로 저장하는 메서드 호출
    }
    private void ActiveStage(int stage)
    {
        for (int i = 0; i < stageMaps.Count; i++)
        {
            stageMaps[i].SetActive(i == stage); //i번째가 해당 stage와 일치하는 것만 SetActive시키기
        }

    }
}
