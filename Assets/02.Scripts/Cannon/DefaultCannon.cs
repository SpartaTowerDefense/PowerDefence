using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCannon : CannonBase
{
    // ĳ�� 1��
    // � �ٵ� ���� ���ҽ��Ŵ����� �ִ� ���� �����ò���
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
