using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    private Quaternion tankRotation;
    [SerializeField] private GameObject seletedEnemy;

    public Collider2D[] enemyColliders;

    private void Start()
    {
        tankRotation = transform.rotation;
        enemyColliders = new Collider2D[10];
    }

    private void Update()
    {
        if(Physics2D.OverlapCircle(transform.position, range,enemyLayer))
        {
            //한놈을 선택하기
            if (seletedEnemy == null)
            {
                SelectEnemy();
            }
            else
            {   //선택된 적이 범위 내에 있는지 검사
                RemoveSeletedEnemy();
                //적을 바라보기
                ChasingEnemy();
            }
                
        }
        else
        {
            //범위내에 적이 없으면 회전값 원상복귀
            transform.rotation = tankRotation;
        }
    }

    // 범위 그리기
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // 공격할 적 선택
    void SelectEnemy()
    {
        enemyColliders = Physics2D.OverlapCircleAll(transform.position, range, enemyLayer);
        seletedEnemy = enemyColliders[enemyColliders.Length-1].gameObject;
        if(enemyColliders.Length > 0) 
            Debug.Log(seletedEnemy);
    }

    // 적 바라보기
    void ChasingEnemy()
    {
        if (seletedEnemy != null)
        {
            Vector2 lookPos = seletedEnemy.transform.position - transform.position;
            float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);
        }
        
    }

    void RemoveSeletedEnemy()
    {
        if (Mathf.Floor(Vector2.Distance(seletedEnemy.transform.position, transform.position) * 10000f) / 10000f >= 3f)
        {
            seletedEnemy = null;
        }
        
    }
}
