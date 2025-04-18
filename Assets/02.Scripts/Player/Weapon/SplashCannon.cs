using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SplashCannon : CannonBase
{
    private Transform tp;
    private int count = 0;
    public SplashCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        data.Inintionalize(3, 0.8f, false, 3f);
        tp = tip;
    }

    GameObject bullet = null;
    public override void Fire(Vector3 targetPos)
    {
        base.Fire(targetPos);

        if (time > 0)
            return;

        if (continous_Time > 0)
            return;

        controller.DetectEnemy.SelectEnemy();
        bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>(1);
        Bullet bul = bullet.GetComponent<Bullet>();
        bul.controller = this.controller;
        bul.SplashRatio = data.SplashRatio;
        bullet.transform.position = tp.position;

        Vector2 lookPos = targetPos - tp.position;
        float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

        bul.rb.gravityScale = 0f;
        bul.rb.AddForce((targetPos - bullet.transform.position).normalized * bul.bulletSpeed, ForceMode2D.Impulse);

        if (count < data.BulletCount)
        {
            count++;
            if (controller.DetectEnemy.enemyColliders.Length >= data.BulletCount)
            {
                //한발씩 쏘기
                controller.DetectEnemy.seletedEnemy = controller.DetectEnemy.enemyColliders[count - 1];
            }
            else if (controller.DetectEnemy.enemyColliders.Length < data.BulletCount && controller.DetectEnemy.enemyColliders.Length > 1)
            {
                controller.DetectEnemy.seletedEnemy = controller.DetectEnemy.enemyColliders[Mathf.FloorToInt(count / 2)];
            }
            else
            {
                controller.DetectEnemy.SelectEnemy();
            }
            continous_Time = continous_CoolDown;
            return;
        }
        else
        {
            isContinousShooting = false;
            count = 1;
            time = fireColldown;
            controller.DetectEnemy.SelectEnemy();
            return;
        }

    }

}
