using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    // 캐논 1렙
    // 어떤 바디에 따라서 리소스매니저에 있는 색깔 가져올꺼임
    public DefaultCannon(int bulletCount, float splashRatio, bool canPenetration) : base(bulletCount, splashRatio, canPenetration)
    {
    }

    public override void Fire()
    {
        throw new System.NotImplementedException();
    }

    public override void Initionalize()
    {
        throw new System.NotImplementedException();
    }
}
