using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCannon : CannonBase
{
    public MeleeCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        data.Inintionalize(0, 0, false);
        controller.DetectEnemy.SetRange(3f);
    }
    public override void Fire(Vector3 targetPos)
    {
        base.Fire(targetPos);

        if (time > 0f)
            return;

        controller.DetectEnemy.SelectEnemy();
        foreach (Collider2D collider in controller.DetectEnemy.enemyColliders)
        {
            if (collider.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
            {
                DefaultAttack(enemy, controller.turretdata.Type);
            }
        }

        time = meleeCoolDown;

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
                enemy.TakeDamage(controller.turretdata.Attack);
                if (enemy.GetHealth() < 0)
                    enemy.RewardModifier = controller.turretdata.Coin;
                break;
            default:
                Debug.Log("터렛타입이 잘못됨");
                break;
        }
    }
}
