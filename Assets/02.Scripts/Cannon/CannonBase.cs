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
}

public abstract class CannonBase
{
    public CannonData data;
    public Transform tip; // 총알 나가는 위치
    // public AudioClip clip; 총알 발사시 소리

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

    public void SetData(int bulletCount, float splashRatio, bool canPenetration)
    {
        data.bulletCount = bulletCount;
        data.splashRatio = splashRatio;
        data.canPenetration = canPenetration;
    }

    public abstract void Fire(Vector3 targetPos);
}
