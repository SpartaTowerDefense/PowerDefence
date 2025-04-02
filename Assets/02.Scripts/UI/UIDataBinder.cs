using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDataBinder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthValue;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI stageNum;
    [SerializeField] private TextMeshProUGUI enemyCount;

    //private Commander commander;

    public void Init()
    {
        //commander = UIManager.Instance.Commander;
        SetUIText();
    }
    public void SetUIText()
    {
        Commander commander = GameManager.Instance.commander;
        healthValue.text = commander.health.ToString();
        goldValue.text = commander.gold.ToString();
        stageNum.text = (GameManager.Instance.CurrentStage+1).ToString();

        var spawner = GameManager.Instance.EnemySpawner;
        enemyCount.text = spawner != null
            ? spawner.AliveEnemyCount.ToString()
            : "-";
    }
}
