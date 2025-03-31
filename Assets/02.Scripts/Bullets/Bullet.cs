using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //private Turret turret;
    public Rigidbody2D rb;
    private Collider2D collider;
    public float bulletSpeed = 8f;
    public CannonController controller;
    private Collider2D[] splashColiders;
    [SerializeField] LayerMask enemyLayer;
    public float SplashRatio { get; set; }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (collider == null) collider = GetComponent<Collider2D>();
        splashColiders = new Collider2D[5];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            CannonData currentData = controller.CurrentCannon.GetData();
            //적 정보를 가져온다
            if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {//적 정보를 가져와서 turret에 잇는 body head에 따른 데미지를 준다  
                //스플래시인지 아닌지 체크
                if (SplashRatio > 0)
                    SplashAttack(enemy, controller.turretdata.Type);
                else
                    DefaultAttack(enemy, controller.turretdata.Type);
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

    void DefaultAttack(Enemy enemy, Enums.TurretType turretType)
    {

        switch (turretType)
        {
            case Enums.TurretType.White:
                enemy.TakeDamage(controller.turretdata.Attack);
                enemy.ApplyKnockback(controller.turretdata.Knockback);
                break;
            case Enums.TurretType.Blue:
                enemy.TakeDamage(controller.turretdata.Attack);
                enemy.ApplyFrozen(controller.turretdata.Flinch); // turretdata에서 얼음 관련 스탯이 먼지 알아야함
                break;
            case Enums.TurretType.Red:
                enemy.TakeDamage(controller.turretdata.Attack);
                enemy.ApplyBurning(controller.turretdata.DotDamage);
                break;
            case Enums.TurretType.Black:
                enemy.TakeDamage(controller.turretdata.Attack); // 임시 변수
                break;
            case Enums.TurretType.Green:
                break;
            default:
                Debug.Log("터렛타입이 잘못됨");
                break;
        }
    }

    void SplashAttack(Enemy enemy, Enums.TurretType turretType)
    {
        splashColiders = Utils.OverlapCircleAllSorted(enemy.transform.position, SplashRatio, enemyLayer, this.transform.position);
        foreach (Collider2D collider in splashColiders)
        {
            Debug.Log($"스플래시 공격 받은 적 {collider}");
            if (collider.TryGetComponent<Enemy>(out Enemy enemies))
            {
                DefaultAttack(enemies, turretType);

            }
        }
    }

}
