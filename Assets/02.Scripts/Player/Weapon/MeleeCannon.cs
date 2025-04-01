using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCannon : CannonBase
{
    public MeleeCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        data.Inintionalize(0, 0, false, 3f);
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
                enemy.DefalutAttack(controller.turretdata.Attack,controller.turretdata);
                break;
            default:
                Debug.Log("터렛타입이 잘못됨");
                break;
        }
    }
}
