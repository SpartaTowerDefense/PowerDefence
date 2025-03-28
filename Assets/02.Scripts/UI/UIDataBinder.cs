using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDataBinder : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthValue;
    [SerializeField] private TextMeshProUGUI goldValue;
    [SerializeField] private TextMeshProUGUI waveNum;
    [SerializeField] private TextMeshProUGUI enemyCount;

    private Commander commander;

    public void Init()
    {
        commander = UIManager.Instance.Commander;
        SetUIText();
    }
    public void SetUIText()
    {
        healthValue.text = commander.health.ToString();
        goldValue.text = commander.gold.ToString();
        waveNum.text = commander.wave.ToString();
        //enemyCount.text = 
    }
}
