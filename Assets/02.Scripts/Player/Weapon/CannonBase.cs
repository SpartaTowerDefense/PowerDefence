using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
    public CannonData data = new(1,0,false);
    public Transform tip; // 총알 나가는 위치
    public CannonController controller;
    // public AudioClip clip; 총알 발사시 소리

    protected float time = 0f;
    protected float continous_CoolDown = 0.3f;
    protected float continous_Time = 0.0f;
    protected float fireColldown = 2f;
    protected float meleeCoolDown = 3f;
    protected bool isContinousShooting = false;

    public CannonBase(Sprite sprite, Transform tip, CannonController controller)
    {
        ChangeSprite(sprite);
        data.tip = tip;
        this.controller = controller;
    }

    public void ChangeSprite(Sprite cannonSprite)
    {
        data.cannonSprite = cannonSprite;
    }

    public void Update()
    {
        time -= Time.deltaTime;
        continous_Time -= Time.deltaTime;
    }

    public CannonData GetData()
    {
        return data;
    }

    public abstract void Fire(Vector3 targetPos);
}
