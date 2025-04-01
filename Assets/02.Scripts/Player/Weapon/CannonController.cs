using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    private const int InitPrice = 1500;
    public int Price { get; private set; } = InitPrice;
    
    public int level = 1;

    public Transform tip;
    public SpriteRenderer spr;

    public GameObject muzzleObject; // muzzleFlash 이펙트 오브젝트
    private WaitForSeconds muzzleWaitFor = new(0.05f);

    public DetectEnemy DetectEnemy { get; private set; }
    public TurretData turretdata;
    
    public CannonBase CurrentCannon { get; set; }
    private DefaultCannon DefaultCannon { get; set; }
    private TripleCannon TripleCannon { get; set; }
    private SplashCannon SplashCannon { get; set; }
    private PenetrationCannon PenetrationCannon { get; set; }
    private MeleeCannon MeleeCannon { get; set; }

    private CannonBase[] cannonList;

    // Cannon Sprites
    Sprite[] sprites = new Sprite[3];

    private void Awake()
    {
        DetectEnemy = GetComponent<DetectEnemy>();
    }

    public void Initinalize(TurretData data)
    {
        turretdata = data;
        sprites[0] = data.LEVEL0;
        sprites[1] = data.LEVEL1;
        sprites[2] = data.LEVEL2;

        if(DefaultCannon == null)
        {
            DefaultCannon = new DefaultCannon(sprites[0], tip, this);

            TripleCannon = new TripleCannon(sprites[1], tip, this);

            SplashCannon = new SplashCannon(sprites[1], tip, this);

            PenetrationCannon = new PenetrationCannon(sprites[1], tip, this);

            MeleeCannon = new MeleeCannon(sprites[2], tip, this);
        }
        else
        {
            DefaultCannon.ChangeSprite(sprites[0]);
            TripleCannon.ChangeSprite(sprites[1]);
            SplashCannon.ChangeSprite(sprites[1]);
            PenetrationCannon.ChangeSprite(sprites[1]);
            MeleeCannon.ChangeSprite(sprites[2]);
        }

        cannonList = new CannonBase[] { DefaultCannon, TripleCannon, SplashCannon, PenetrationCannon, MeleeCannon };
        level = 1;
        ChangeSprite();
        Price = InitPrice;
        //ChangeCannon(TripleCannon);
    }

    public void Update()
    {
        if (CurrentCannon != null)
            CurrentCannon.Update();

        if (DetectEnemy.seletedEnemy != null)
        {
            Fire();
        }
            
    }

    /// <summary>
    /// UI에서 포씬을 강화했을때 호출 ( 매개변수는 CannonController에 참조 )
    /// </summary>
    public void ChangeCannon()
    {
        Commander commander = GameManager.Instance.commander;

        if(level < cannonList.Length && commander.CanBuy(Price))
        {
            level++;
            ChangeSprite();

            if (level > 1)
            {
                commander.SubtractGold(Price); // 먼저 차감
                SetPriceRatio(1.2f); // 현재 가격에서 증가
                SetRange(CurrentCannon.data.Range);
            }

            Debug.Log($"선택된 캐논 : {CurrentCannon}");

        }
    }

    private void ChangeSprite()
    {
        CurrentCannon = cannonList[level - 1];
        CurrentCannon.OnMuzzleFlash = OnMuzzleFlash;
        spr.sprite = CurrentCannon.data.cannonSprite;
    }

    public void Fire()
    {
        if (CurrentCannon != null)
        {
            CurrentCannon.Fire(DetectEnemy.seletedEnemy.transform.position);
        }
    }

    private void OnMuzzleFlash()
    {
        StartCoroutine(MuzzleFlash());
    }

    private IEnumerator MuzzleFlash()
    {
        muzzleObject.SetActive(true);
        yield return muzzleWaitFor;
        muzzleObject.SetActive(false);
    }

    // 가격 설정
    public void SetPriceRatio(float ratio)
    {
        this.Price = Mathf.FloorToInt(this.Price * ratio);
    }

    public void SetRange(float range)
    {
        DetectEnemy.SetRange(range);
    }
}
