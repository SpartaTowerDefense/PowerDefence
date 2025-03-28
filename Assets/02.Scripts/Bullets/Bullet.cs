using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    private Turret turret;
    private Rigidbody2D rb;
    private Collider2D collider;
    private float bulletSpeed = 5f;

    GameObject enemy;
    Vector3 targetPos;
    Vector2 lookPos;


    private void Start()
    {
        if (!transform.parent.TryGetComponent<Turret>(out turret))
        {
            //부모의 터렛 스크립트를 찾지 못했을떄
            //오브젝트 풀에 반납한다.
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
            Debug.Log("터렛 스크립트를 찾지 못함");
            return;
        }
        GameObject targetEnemy = turret.detectEnemy.seletedEnemy;
        if (targetEnemy == null)
        {
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
            Debug.LogWarning("적이 없습니다");
            return;
        }

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (collider == null) collider = GetComponent<Collider2D>();

        //transform.position = transform.parent.position;
        //transform.parent = null;

        // 위치 복사해서 저장
        targetPos = targetEnemy.transform.position;
        lookPos = targetPos - transform.position;
        float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

        rb.gravityScale = 0f;
        rb.AddForce((targetPos - transform.position).normalized * bulletSpeed,ForceMode2D.Impulse);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            //적 정보를 가져온다
            //적 정보를 가져와서 turret에 잇는 body head에 따른 데미지를 준다

            if (!turret.CanPenetration) // 만약 관통속성이 false라면
            {
                //오브젝트풀에 반납한다.
                ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
            }
        }

        //만약 탄환이 맵의 경계선을 만나면
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            //오브젝트 풀에 반납한다
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
        }

    }
   
}
