using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Bullet : MonoBehaviour
{
    //private Turret turret;
    public Rigidbody2D rb;
    private Collider2D collider;
    public float bulletSpeed = 5f;
    public CannonController controller;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (collider == null) collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            CannonData currentData = controller.CurrentCannon.GetData();
            //적 정보를 가져온다
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {//적 정보를 가져와서 turret에 잇는 body head에 따른 데미지를 준다  
                enemy.TakeDamage(controller.turretdata.Attack);
                Debug.Log($"적 체력 : {enemy.Health}");
            }
            

            if (!currentData.CanPenetration) // 만약 관통속성이 false라면
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
