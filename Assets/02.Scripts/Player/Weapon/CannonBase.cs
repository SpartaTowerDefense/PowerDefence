using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class CannonData
{
    public int BulletCount { get; set; }
    public float SplashRatio { get; set; }
    public bool CanPenetration { get; set; }

    public Sprite cannonSprite;
    public Transform tip;

    public CannonData(int bulletCount, float splashRatio, bool canPenetration)
    {
        Inintionalize(bulletCount, splashRatio, canPenetration);
    }

    public void Inintionalize(int bulletCount, float splashRatio, bool canPenetration)
    {
        BulletCount = bulletCount;
        SplashRatio = splashRatio;
        CanPenetration = canPenetration;
    }
}

public abstract class CannonBase
{
    public CannonData data = new(1,0f, false);
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

    public CannonData GetData()
    {
        return data;
    }

    public abstract void Fire();
}
