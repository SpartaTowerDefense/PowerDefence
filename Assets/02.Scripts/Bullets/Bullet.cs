using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Turret turret;
    private Rigidbody rb;
    private Collider2D collider;
    [SerializeField] private float bulletSpeed = 2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider2D>();
    }
    // 비활성상태에서 활성화 됐을떄(발사 됐을 떄)
    private void OnEnable()
    {
        if(!transform.parent.TryGetComponent<Turret>(out turret))
        {
            //부모의 터렛 스크립트를 찾지 못했을떄
            //다시 오브젝트 풀에 반납
            Debug.Log("터렛 스크립트를 찾지 못함");
            return;
        }
        else
        {
            //발사
            //turret.detectEnemy.seletedEnemy.transform 를 향해    
            rb.AddForce(Vector2.right * bulletSpeed);
        }
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            //적 정보를 가져온다
            //적 정보를 가져와서 turret에 잇는 body head에 따른 데미지를 준다
            
            if (!turret.CanPenetration) // 만약 관통속성이 false라면
            {
                //오브젝트풀에 반납한다.
            }
        }

        //만약 탄환의 맵의 경계선을 벗어나면
        //다시 오브젝트 풀에 반납한다
        
    }

    
}
