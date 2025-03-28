using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// 임시방편
[System.Serializable]
public class TurretStatus
{
    public int Level { get; private set; }
    public int Price { get; private set; }
    public float Attack { get; private set; }
    public float DotDamage { get; private set; }
    public float Flinch { get; private set; }
    public float Knockback { get; private set; }
    public float Coin { get; private set; } // 코인 획득량 배율
    public float Duration { get; private set; }

    public TurretStatus(TurretData data)
    {
        Initinalize(data);
    }

    public void Initinalize(TurretData data)
    {
        Level = 1;
        this.Price = data.Price;
        this.Attack = data.Attack;
        this.DotDamage = data.DotDamage;
        this.Flinch = data.Flinch;
        this.Knockback = data.Knockback;
        this.Coin = data.Coin;
        this.Duration = data.Duration;
    }

    public void LevelUp(int price, float attack, float dotDamage, float flinch,
        float knockback, float coin, float duration)
    {
        Level++;
        this.Price += price;
        this.Attack += attack;
        this.DotDamage += dotDamage;
        this.Flinch += flinch;
        this.Knockback += knockback;
        this.Coin += coin;
        this.Duration += duration;
    }

    public TurretStatus GetStatus()
    {
        return this;
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

    #endregion

    #region BodyDatas
    public Enums.TurretType Type { get; private set; } = Enums.TurretType.Black;
    public int Level { get; set; } = 1; // 바디에 대한 레벨

    // 변할 수 있는 배율 TurretData 직접 호출X TurretData는 디폴트 값
    public TurretStatus TurretStat { get; private set; } = null;

    #endregion


    private void Awake()
    {
        // 컴포넌트 초기화
        bodySpr = transform.GetComponentInChildren<SpriteRenderer>(true);
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

        if (TurretStat == null)
            TurretStat = new(data);
        else
            TurretStat.Initinalize(data);

        bodySpr.sprite = data.BodyImage;
    }
}
