using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform tip;
    public SpriteRenderer spr;

    public DetectEnemy DetectEnemy { get; private set; }
    public TurretData turretdata;
    
    public CannonBase CurrentCannon { get; set; }
    private DefaultCannon DefaultCannon { get; set; }
    private TripleCannon TripleCannon { get; set; }

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
            // ì²«ë²ˆì§¸ ìºë…¼
            DefaultCannon = new DefaultCannon(sprites[0], tip, this);

            // ë‘ë²ˆì¬ ìºë…¼
            TripleCannon = new TripleCannon(sprites[1], tip, this);

            // ì„¸ë²ˆì§¸ ìºë…¼
        }
        else
        {
            DefaultCannon.ChangeSprite(sprites[0]);
            TripleCannon.ChangeSprite(sprites[1]);
        }

        ChangeCannon(DefaultCannon);
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
    /// UI¿¡¼­ Æ÷¾ÀÀ» °­È­ÇßÀ»¶§ È£Ãâ ( ¸Å°³º¯¼ö´Â CannonController¿¡ ÂüÁ¶ )
    /// </summary>
    /// <param name="changeCannon"></param>
    public void ChangeCannon(CannonBase changeCannon)
    {
        CurrentCannon = changeCannon;
        spr.sprite = changeCannon.data.cannonSprite;
    }

    public void Fire()
    {
        if (CurrentCannon != null)
            CurrentCannon.Fire(DetectEnemy.seletedEnemy.transform.position);

        
    }
}
