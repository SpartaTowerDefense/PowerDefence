using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    public DefaultCannon(Sprite sprite, Transform tip) : base(sprite, tip)
    {
        // 디폴트 총은 총알 개수 1개, 스플래쉬 반지름 범위 0, 관통x
        data.Inintionalize(1, 0, false);
    }

    public override void Fire()
    {
        if (time > 0f)
            return;

        GameObject bullet = null;
        for (int i = 0; i < data.BulletCount; i++)
        {
            bullet = ObjectPoolManager.Instance.GetObject<BulletFactory>();
            bullet.GetComponent<Bullet>().LaunchBullet();
        }
    }
}
