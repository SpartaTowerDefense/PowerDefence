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
    [SerializeField] private bool hasHit = false;
    public float SplashRatio { get; set; }

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (collider == null) collider = GetComponent<Collider2D>();
        splashColiders = new Collider2D[5];
    }

    private void OnEnable()
    {
        hasHit = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CannonData currentData = controller.CurrentCannon.GetData();

        //만약 탄환이 맵의 경계선을 만나면
        if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))
        {
            //오브젝트 풀에 반납한다
            ObjectPoolManager.Instance.ReturnObject<BulletFactory>(this.gameObject);
        }

        if (hasHit && !currentData.CanPenetration) return;

        if (collision.gameObject.tag.Equals("Enemy"))
        {    
            hasHit = true;
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

    }

    void DefaultAttack(Enemy enemy, Enums.TurretType turretType)
    {

        switch (turretType)
        {
            case Enums.TurretType.White:
                enemy.ApplyKnockback(controller.turretdata.Knockback, controller.turretdata.Attack);
                break;
            case Enums.TurretType.Blue:
                enemy.ApplyFrozen(controller.turretdata.Flinch, controller.turretdata.Attack);
                break;
            case Enums.TurretType.Red:
                enemy.ApplyBurning(controller.turretdata.DotDamage, controller.turretdata.Attack);
                break;
            case Enums.TurretType.Black:
                enemy.DefalutAttack(controller.turretdata.Attack);
                break;
            case Enums.TurretType.Green:
                enemy.DefalutAttack(controller.turretdata.Attack, controller.turretdata);
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
            if (collider.TryGetComponent<Enemy>(out Enemy enemies))
            {
                DefaultAttack(enemies, turretType);

            }
        }
    }

}
