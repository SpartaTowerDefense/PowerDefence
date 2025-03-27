using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    DetectEnemy detectEnemy;
    [SerializeField] private GameObject bulletPrefabs;

    private void Start()
    {
        detectEnemy = GetComponent<DetectEnemy>();
    }

    private void Update()
    {   //감지된 적이 있다면
        if(detectEnemy.seletedEnemy!=null)
        {
            //포탄 발싸-> 어떤 포탄을 발싸하냐 -> 탱크 속성과 포신의 종류에 따라 -> 포탄을 
        }
    }
}
