using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using System.Linq;

public class DetectEnemy : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float range;
    private Quaternion tankRotation;
    public Collider2D seletedEnemy;

    public Collider2D[] enemyColliders;
    private CannonController controller;

    private void Start()
    {
        tankRotation = transform.rotation;
        controller = GetComponent<CannonController>();
        enemyColliders = new Collider2D[10];
    }

    private void Update()
    {
        //범위 내에 적이 진입했는지 체크
        if (Physics2D.OverlapCircle(transform.position, range, enemyLayer))
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
            Array.Clear(enemyColliders, 0, enemyColliders.Length);
        }
    }

    // 범위 그리기
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // 공격할 적 선택
    public void SelectEnemy()
    {
        enemyColliders = OverlapCircleAllSorted(transform.position, range, enemyLayer);

        if (enemyColliders.Length > 0)
        {
            seletedEnemy = enemyColliders[0];
            Debug.Log($"선택된 적 : {seletedEnemy}");
            
        }
    }
    // 적 바라보기
    void ChasingEnemy()
    {
        if (seletedEnemy != null)
        {
            Vector2 lookPos = seletedEnemy.gameObject.transform.position - transform.position;
            float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);
        }
    }

    //적 범위 벗어나는거 체크
    void RemoveSeletedEnemy()
    {
        if (Mathf.Floor(Vector2.Distance(seletedEnemy.gameObject.transform.position, transform.position) * 10000f) / 10000f >= range)
        {
            seletedEnemy = null;
        }

    }

    public Collider2D[] OverlapCircleAllSorted(Vector2 center, float radius, int layerMask)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, layerMask);

        return colliders
            .OrderBy(c => Vector2.SqrMagnitude((Vector2)c.transform.position - (Vector2)transform.position))
            .ToArray();
    }
}
