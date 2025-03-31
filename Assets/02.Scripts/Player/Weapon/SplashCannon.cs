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
        data.Inintionalize(3, 3, false);
        controller.DetectEnemy.SetRange(4f);
        tp = tip;
    }

    GameObject bullet = null;
    public override void Fire(Vector3 targetPos)
    {
        if (time > 0f && continous_Time > 0)
            return;

        controller.DetectEnemy.SelectEnemy(1);
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
            continous_Time = continous_CoolDown;
            //controller.DetectEnemy.SelectEnemy(1);
            return;
        }
        else
        {
            count = 0;
            continous_Time = 0f;
            time = fireColldown;
            controller.DetectEnemy.SelectEnemy(1);
            return;
        }

    }

}
