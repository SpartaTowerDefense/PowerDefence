using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// 임시방편
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
    public CannonController CannonCtrl { get; private set; }

    public DetectEnemy detectEnemy;

    #endregion

    #region BodyDatas
    public Enums.TurretType Type { get; private set; } = Enums.TurretType.Black;
    public int Level { get; set; } = 1;
    public int Price { get; private set; } = 1000;

    // 변할 수 있는 배율
    public float AttackRatio { get; private set; } = 1f;
    public float DotDamageRatio { get; private set; } = 1f;
    public float FlinchRatio { get; private set; } = 1f;
    public float KnockbackRatio { get; private set; } = 1f;
    public float CoinRatio { get; private set; } = 1f;
    public float AbilityDuration { get; private set; } = 1f;

    #endregion


    // HeadData경우에는 필요가 없을꺼 같아서 이건 삭제를 해야됨
    // HeadData의 정보들은 CannonController의 CurrentCannon에 담겨져 있습니다.
    #region HeadDatas
    public int BulletCount { get; private set; } =  1;
    public float SplashRatio { get; private set; } = 1f;
    public bool CanPenetration { get; private set; } = false;

    private Sprite[] cannonArr = new Sprite[3];

    #endregion

    // Turret Default Status
    [field:SerializeField] public TurretStatus DefaultStatus { get; private set; }


    private void Awake()
    {
        // 컴포넌트 초기화
        detectEnemy = GetComponent<DetectEnemy>();
        bodySpr = transform.GetComponentInChildren<SpriteRenderer>(true);

        CannonCtrl = GetComponent<CannonController>();
    }

    /// <summary>
    /// [사용안함] BulletCount, SplashRatio, CanPenetration
    /// </summary>
    /// <param name="data"></param>
    public void Initinalize(TurretData data)
    {
        // 디폴트 값 데이터
        if (data == null)
            return;

        AttackRatio = data.Attack;
        DotDamageRatio = data.DotDamage;
        FlinchRatio = data.Flinch;
        KnockbackRatio = data.Knockback;
        CoinRatio = data.Coin;
        AbilityDuration = data.Duration;

        // Cannon
        BulletCount = data.BulletCount;
        SplashRatio = data.SplashRatio;
        CanPenetration = data.CanPenetration;

        cannonArr[0] = data.LEVEL0;
        cannonArr[1] = data.LEVEL1;
        cannonArr[2] = data.LEVEL2;

        bodySpr.sprite = data.BodyImage;
        headSpr.sprite = data.LEVEL0;
    }


}
