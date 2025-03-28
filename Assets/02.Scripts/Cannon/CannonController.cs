using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform tip;
    public SpriteRenderer spr;

    private DetectEnemy detectEnemy;
    
    public CannonBase CurrentCannon { get; set; }
    private DefaultCannon DefaultCannon { get; set; }
    private TripleCannon TripleCannon { get; set; }

    // Cannon Sprites
    Sprite[] sprites = new Sprite[3];

    private void Awake()
    {
        detectEnemy = GetComponent<DetectEnemy>();
    }

    public void Initinalize(TurretData data)
    {
        sprites[0] = data.LEVEL0;
        sprites[1] = data.LEVEL1;
        sprites[2] = data.LEVEL2;

        if(DefaultCannon == null)
        {
            // 첫번째 캐논
            DefaultCannon = new DefaultCannon(sprites[0], tip);

            // 두번재 캐논
            TripleCannon = new TripleCannon(sprites[1], tip);

            // 세번째 캐논
        }
        else
        {
            DefaultCannon.ChangeSprite(sprites[0]);
            TripleCannon.ChangeSprite(sprites[1]);
        }

        ChangeCannon(DefaultCannon);
    }

    public void Update()
    {
        if (CurrentCannon != null)
        CurrentCannon.Update();

        //if (detectEnemy.seletedEnemy != null)
            //Fire();
    }

    public void ChangeCannon(CannonBase changeCannon)
    {
        CurrentCannon = changeCannon;
        spr.sprite = changeCannon.data.cannonSprite;
    }

    public void Fire()
    {
        if (CurrentCannon != null)
            CurrentCannon.Fire(detectEnemy.seletedEnemy.transform.position);
    }
}
