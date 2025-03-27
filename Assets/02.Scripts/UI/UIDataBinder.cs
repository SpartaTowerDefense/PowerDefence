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

    public void SetUIText()
    {
        healthValue.text = UIManager.Instance.Commander.health.ToString();
        goldValue.text = UIManager.Instance.Commander.gold.ToString();
        waveNum.text = UIManager.Instance.Commander.wave.ToString();
        //enemyCount.text = 
    }
}
