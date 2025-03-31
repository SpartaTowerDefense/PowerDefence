using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// 임시방편
[System.Serializable]
public class TurretStatus
{
    public int Price { get; private set; }
    public float Attack { get; set; }
    public float DotDamage { get; set; }
    public float Flinch { get; set; }
    public float Knockback { get; set; }
    public float Coin { get; set; } // 코인 획득량 배율
    public float Duration { get; set; }

    public TurretStatus(TurretData data)
    {
        Initinalize(data);
    }

    public void Initinalize(TurretData data)
    {
        this.Price = data.Price;
        this.Attack = data.Attack;
        this.DotDamage = data.DotDamage;
        this.Flinch = data.Flinch;
        this.Knockback = data.Knockback;
        this.Coin = data.Coin;
        this.Duration = data.Duration;
    }

    public void LevelUp(float ratio)
    {
        this.Attack *= ratio;
        this.DotDamage *= ratio;
        this.Flinch *= ratio;
        this.Knockback *= ratio;
        this.Coin *= ratio;
    }

    public void SetPriceRatio(float ratio)
    {
        this.Price = Mathf.FloorToInt(this.Price * ratio);
    }

    /*public void LevelUp(int price, float attack, float dotDamage, float flinch,
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
    }*/

    public TurretStatus GetStatus()
    {
        return this;
    }
}

public class Turret : MonoBehaviour
{
    #region Componenets
    private SpriteRenderer bodySpr { get; set; }

    #endregion

    #region BodyDatas
    public Enums.TurretType Type { get; private set; } = Enums.TurretType.Black;
    public int Level { get; set; } = 1; // 바디에 대한 레벨
    private int maxLevel = 30;

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

        Level = 1;
        bodySpr.sprite = data.BodyImage;
    }

    public void LevelUp()
    {
        Commander commander = GameManager.Instance.commander;
        if (!commander.CanBuy(TurretStat.Price) && Level > 1)
            return;

        if(Level < maxLevel)
        {
            if (TurretStat == null)
                return;

            Level++;
            TurretStat.LevelUp(1.2f);
            commander.SubtractGold(TurretStat.Price); // 차감 후
            TurretStat.SetPriceRatio(2); // 가격 증가

            UIManager.Instance.UIDataBinder.SetUIText();
        }
    }
}
