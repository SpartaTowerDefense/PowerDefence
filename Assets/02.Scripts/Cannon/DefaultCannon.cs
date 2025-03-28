using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    private Transform tp;
    public DefaultCannon(Sprite sprite, Transform tip, CannonController controller) : base(sprite, tip, controller)
    {
        SetData(1, 0, false);
        tp = tip;
        
    }

    public override void Fire(Vector3 targetPos)
    {
        if (time > 0f)
            return;

        GameObject bullet = null;
        for (int i = 0; i < data.bulletCount; i++)
        {
            bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>(1);
            Bullet bul = bullet.GetComponent<Bullet>();
            bul.controller = this.controller;
            bullet.transform.position = tp.position;

            Vector2 lookPos = targetPos - tp.position;
            float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

            bul.rb.gravityScale = 0f;
            bul.rb.AddForce((targetPos - bullet.transform.position).normalized * bul.bulletSpeed, ForceMode2D.Impulse);
        }
        time = fireColldown;
    }

    
}
