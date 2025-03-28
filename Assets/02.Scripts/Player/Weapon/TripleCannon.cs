using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleCannon : CannonBase
{
    private int count = 0;
    public TripleCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        SetData(3, 0, false);
    }

    GameObject bullet = null;
    public override void Fire(Vector3 targetPos)
    {
        if (time > 0f && continous_Time > 0)
            return;

        bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
        Bullet bul = bullet.GetComponent<Bullet>();
        bullet.transform.position = tip.position;

        Vector2 lookPos = targetPos - tip.position;
        float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

        bul.rb.gravityScale = 0f;
        bul.rb.AddForce((targetPos - bullet.transform.position).normalized * bul.bulletSpeed, ForceMode2D.Impulse);

        if(count < data.bulletCount)
        {
            count++;
            continous_Time = continous_CoolDown;
            controller.DetectEnemy.SelectEnemy();
            return;
        }
        else
        {
            count = 0;
            continous_Time = 0f;
            time = fireColldown;
            controller.DetectEnemy.SelectEnemy();
            return;
        }
        
    }
}
