using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    public DefaultCannon(Sprite sprite, Transform tip) : base(sprite, tip)
    {
        SetData(1, 0, false);
    }

    public override void Fire(Vector3 targetPos)
    {
        if (time > 0f)
            return;

        GameObject bullet = null;
        for (int i = 0; i < data.bulletCount; i++)
        {
            bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
            Bullet bul = bullet.GetComponent<Bullet>();
            bullet.transform.position = tip.position;

            Vector2 lookPos = targetPos - tip.position;
            float rotz = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, rotz + 90f);

            bul.rb.gravityScale = 0f;
            bul.rb.AddForce((targetPos - bullet.transform.position).normalized * bul.bulletSpeed, ForceMode2D.Impulse);
        }
    }

    
}
