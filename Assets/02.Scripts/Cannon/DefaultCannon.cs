using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    public DefaultCannon(Sprite sprite, Transform tip) : base(sprite, tip)
    {
        SetData(1, 0, false);
    }

    public override void Fire()
    {
        if (time > 0f)
            return;

        GameObject bullet = null;
        for (int i = 0; i < data.bulletCount; i++)
        {
            bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
            bullet.GetComponent<Bullet>().LaunchBullet();
        }
    }
}
