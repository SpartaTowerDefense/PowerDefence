using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public struct CannonData
{
    public int bulletCount;
    public float splashRatio;
    public bool canPenetration;

    public Sprite cannonSprite;
    public Transform tip;


    public void InitData(int bulletCount, float splashRatio, bool canPenetration)
    {
        this.bulletCount = bulletCount;
        this.splashRatio = splashRatio;
        this.canPenetration = canPenetration;
    }
}

public abstract class CannonBase
{
    public CannonData data;
    public Transform tip; // �Ѿ� ������ ��ġ
    // public AudioClip clip; �Ѿ� �߻�� �Ҹ�

    protected float time = 0f;
    protected float fireColldown = 1f;

    public CannonBase(Sprite sprite, Transform tip)
    {
        ChangeSprite(sprite);
        data.tip = tip;
    }

    public void ChangeSprite(Sprite cannonSprite)
    {
        data.cannonSprite = cannonSprite;
    }

    public void Update()
    {
        time -= Time.deltaTime;
    }

    public abstract void Fire();
}
