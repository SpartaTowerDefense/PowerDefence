using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// �ӽù���
[System.Serializable]
public class TurretStatus
{
    public float attack;
    public float dotDamage;
    public float flinch;
    public float knockback;
    public int coin;
    public float duration;

    public float bulletCount;
    public float splashRatio;
    public float canPentration;

    public TurretStatus(float attack, float dotDamage, float flinch, 
        float knockback, int coin, float duration, int bulletCount, float splashRatio, bool canPentration)
    {
        this.attack = attack;
        this.dotDamage = dotDamage;
        this.flinch = flinch;
        this.knockback = knockback;
        this.coin = coin;
        this.duration = duration;
    }
}

public class Turret : MonoBehaviour
{
    #region StringKeys
    private const string Body = nameof(Body);
    private const string Head = nameof(Head);
    #endregion

    #region Componenets
    private SpriteRenderer bodySpr { get; set; }
    private SpriteRenderer headSpr { get; set; }
    public DetectEnemy detectEnemy;
    #endregion

    #region BodyDatas
    public Enums.TurretType Type { get; private set; } = Enums.TurretType.Black;
    public int Price { get; private set; } = 1000;

    // ���� �� �ִ� ����
    public float AttackRatio { get; private set; } = 1f;
    public float DotDamageRatio { get; private set; } = 1f;
    public float FlinchRatio { get; private set; } = 1f;
    public float KnockbackRatio { get; private set; } = 1f;
    public float CoinRatio { get; private set; } = 1f;
    public float AbilityDuration { get; private set; } = 1f;

    #endregion

    #region HeadDatas
    public int BulletCount { get; private set; } =  1;
    public float SplashRatio { get; private set; } = 1f;
    public bool CanPenetration { get; private set; } = false;

    #endregion

    // Turret Default Status
    [field:SerializeField] public TurretStatus DefaultStatus { get; private set; }


    private void Awake()
    {
        // ������Ʈ �ʱ�ȭ
        detectEnemy = GetComponent<DetectEnemy>();
        bodySpr = transform.GetComponentInChildren<SpriteRenderer>(Body);
        headSpr = transform.GetComponentInChildren<SpriteRenderer>(Head);
    }

    /// <summary>
    /// Factory���� �ʱ�ȭ �۾�
    /// </summary>
    /// <param name="data"></param>
    public void Initinalize(TurretData data)
    {
        // ����Ʈ �� ������
        if (data == null)
            return;

        AttackRatio = data.Attack;
        DotDamageRatio = data.DotDamage;
        FlinchRatio = data.Flinch;
        KnockbackRatio = data.Knockback;
        CoinRatio = data.Coin;
        AbilityDuration = data.Duration;

        BulletCount = data.BulletCount;
        SplashRatio = data.SplashRatio;
        CanPenetration = data.CanPenetration;
    }
    
}
