using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public Transform tip;
    public SpriteRenderer spr;
    
    public CannonBase CurrentCannon { get; set; }
    private DefaultCannon DefaultCannon { get; set; }

    // Cannon Sprites
    Sprite[] sprites = new Sprite[3];

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void Initinalize(TurretData data)
    {
        sprites[0] = data.LEVEL0;
        sprites[1] = data.LEVEL1;
        sprites[2] = data.LEVEL2;

        if(DefaultCannon == null)
        {
            DefaultCannon = new DefaultCannon(sprites[0], tip);
            // �ι��� ĳ��
            // ����° ĳ��
        }
        else
        {
            DefaultCannon.ChangeSprite(sprites[0]);
        }

        ChangeCannon(DefaultCannon);
    }

    public void Update()
    {
        if (CurrentCannon != null)
            CurrentCannon.Update();
    }

    public void ChangeCannon(CannonBase changeCannon)
    {
        CurrentCannon = changeCannon;
        spr.sprite = changeCannon.data.cannonSprite;
    }

    public void Fire()
    {
        if (CurrentCannon != null)
            CurrentCannon.Fire();
    }

}
