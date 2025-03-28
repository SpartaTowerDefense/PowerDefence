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
            //오브젝트 풀에 반납한다.
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
            Debug.Log("터렛 스크립트를 찾지 못함");
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Enemy"))
        {
            //적 정보를 가져온다
            //적 정보를 가져와서 turret에 잇는 body head에 따른 데미지를 준다
            
            if (!turret.HeadData.CanPenetration) // 만약 관통속성이 false라면
            {
                //오브젝트풀에 반납한다.
                ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
            }
        }

        //만약 탄환이 맵의 경계선을 만나면
        if (collision.gameObject.layer == 8)
        {
            //오브젝트 풀에 반납한다
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
        }

    }

    //탄환 발싸
    public void LaunchBullet()
    {
        //터렛이 바라보는 방향으로 발사
        rb.AddForce(Vector2.right * bulletSpeed);
    }

    
}
