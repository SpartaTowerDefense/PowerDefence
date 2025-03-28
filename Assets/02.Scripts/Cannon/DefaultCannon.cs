using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    public DefaultCannon(Sprite sprite, Transform tip) : base(sprite, tip)
    {
        data.InitData(1, 0, false);
    }

    public override void Fire()
    {
        if (time > 0f)
            return;

        GameObject bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
        bullet.GetComponent<Bullet>().LaunchBullet();
    }
}
