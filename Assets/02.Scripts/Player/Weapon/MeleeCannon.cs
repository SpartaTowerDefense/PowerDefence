using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCannon : CannonBase
{
    public MeleeCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        data.Inintionalize(0, 3, false);
    }
    public override void Fire(Vector3 targetPos)
    {
        foreach(Collider2D collider in controller.DetectEnemy.enemyColliders)
        {
            if(collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                DefaultAttack(enemy, controller.turretdata.Type);
            }
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
                enemy.TakeDamage(controller.turretdata.Attack + 5f); // 임시 변수
                break;
            case Enums.TurretType.Green:
                //죽었을때 돈을 더 
                break;
            default:
                Debug.Log("터렛타입이 잘못됨");
                break;
        }

        Debug.Log($"적 체력 : {enemy.Health}");
    }
}
