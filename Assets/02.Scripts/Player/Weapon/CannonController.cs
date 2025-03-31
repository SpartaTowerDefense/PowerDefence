using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public int level = 1;

    public Transform tip;
    public SpriteRenderer spr;

    public DetectEnemy DetectEnemy { get; private set; }
    public TurretData turretdata;
    
    public CannonBase CurrentCannon { get; set; }
    private DefaultCannon DefaultCannon { get; set; }
    private TripleCannon TripleCannon { get; set; }

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
            // 泥ル쾲吏?罹먮끉
            DefaultCannon = new DefaultCannon(sprites[0], tip, this);

            // ?먮쾲??罹먮끉
            TripleCannon = new TripleCannon(sprites[1], tip, this);

            // ?몃쾲吏?罹먮끉
        }
        else
        {
            DefaultCannon.ChangeSprite(sprites[0]);
            TripleCannon.ChangeSprite(sprites[1]);
        }

        cannonList = new CannonBase[] { DefaultCannon, TripleCannon };
        level = 0;
        ChangeCannon();
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
        level = Mathf.Min(++level, cannonList.Length);
        CurrentCannon = cannonList[level - 1];
        spr.sprite = CurrentCannon.data.cannonSprite;
    }

    public void Fire()
    {
        if (CurrentCannon != null)
            CurrentCannon.Fire(DetectEnemy.seletedEnemy.transform.position);
    }
}
