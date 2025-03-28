using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonData
{
    public int BulletCount { get; set; } = 1;
    public float SplashRatio { get; set; } = 0f;
    public bool CanPenetration { get; set; } = false;

    public CannonData(int bulletCount, float splashRatio, bool canPenetration)
    {
        Initionalize(bulletCount, splashRatio, canPenetration);
    }

    public void Initionalize(int defaultCount, float defaultRatio, bool defaultPenetration)
    {
        BulletCount = defaultCount;
        SplashRatio = defaultRatio;
        CanPenetration = defaultPenetration;
    }

    public void ModifiyData(int plusCount, float plusRatio, bool canPenetration)
    {
        BulletCount += plusCount;
        SplashRatio += plusRatio;
        CanPenetration = canPenetration;
    }
}

public abstract class CannonBase
{
    public CannonData data;
    public Transform tip; // 총알 나가는 위치
    // public AudioClip clip; 총알 발사시 소리

    public CannonBase(int bulletCount, float splashRatio, bool canPenetration)
    {
        data = new(bulletCount, splashRatio, canPenetration);
    }

    public abstract void Initionalize();

    public abstract void Fire();
}
