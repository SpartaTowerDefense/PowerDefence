using System;
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
    public float Range { get; set; }

    public Sprite cannonSprite;
    public Transform tip;

    public CannonData(int bulletCount, float splashRatio, bool canPenetration, float range)
    {
        Inintionalize(bulletCount, splashRatio, canPenetration, range);
    }

    public void Inintionalize(int bulletCount, float splashRatio, bool canPenetration, float range)
    {
        BulletCount = bulletCount;
        SplashRatio = splashRatio;
        CanPenetration = canPenetration;
        Range = range;
    }
}

public abstract class CannonBase
{
    public Action OnMuzzleFlash;

    public CannonData data = new(1,0,false,3f);
    public Transform tip; // 총알 나가는 위치
    public CannonController controller;
    // public AudioClip clip; 총알 발사시 소리

    protected float time = 0f;
    protected float continous_CoolDown = 0.3f;
    protected float continous_Time = 0.0f;
    protected float fireColldown = 2f;
    protected float meleeCoolDown = 3f;
    protected bool isContinousShooting = false;

    protected ResourceManager resource;
    protected AudioClip clip;

    public CannonBase(Sprite sprite, Transform tip, CannonController controller)
    {
        resource = ResourceManager.Instance;
        clip = resource.LoadResource<AudioClip>($"{Enums.FireClip}3"); // 공통 총기 사운드
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

    public virtual void Fire(Vector3 targetPos)
    {
        if (time < 0f && continous_Time < 0f)
        {
            AudioManager.Instance.SFXSource.PlayOneShot(clip);
            OnMuzzleFlash?.Invoke();
        }
    }
}
