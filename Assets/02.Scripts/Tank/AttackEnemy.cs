using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : MonoBehaviour
{
    DetectEnemy detectEnemy;
    Turret turret;
    Bullet buletScript;
    int continuosBulletCount = 0;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float delayTimer = 0f;
    [SerializeField] private float Continuous_delayTimer = 0f;
    [SerializeField] private float attackDelay = 1f;
    [SerializeField] private float Continuous_Attack_Delay = 0.2f;

    private void Start()
    {
        detectEnemy = GetComponent<DetectEnemy>();
        turret = GetComponent<Turret>();
    }

    private void Update()
    {   
        Timer();
        //감지된 적이 있다면
        if (detectEnemy.seletedEnemy != null)
        {   // 공격 딜레이가 다 됐다면
            if (delayTimer > attackDelay)
            {
                if (turret.HeadData.BulletCount > 1 && Continuous_delayTimer >= Continuous_Attack_Delay && continuosBulletCount <= turret.HeadData.BulletCount)//탄환수가 여러개고 연속발사 타이머가 0이라면
                {
                    Lauch();
                    Continuous_delayTimer = 0f;
                    continuosBulletCount++;
                    return;
                }
                else if(turret.HeadData.BulletCount == 1)
                {
                    // 아니면 한번만 발사
                    Lauch();
                }
                else if(turret.HeadData.BulletCount > 1 && Continuous_delayTimer < Continuous_Attack_Delay)
                {   // 불릿카운트가 1초과인데 연속공격 타이머가 안된경우 리턴
                    return;
                }
                delayTimer = 0f;
                continuosBulletCount = 0;
            }
        }

        
    }

    void Lauch()
    {
        //오브젝트 풀에서 포탄 가져와서 자식개체로 만든 후 발사
        bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
        bullet.transform.SetParent(transform);
        buletScript = bullet.GetComponent<Bullet>();
        buletScript.LaunchBullet();
    }

    void Timer()
    {
        delayTimer += Time.deltaTime;
        Continuous_delayTimer += Time.deltaTime;
    }
}
