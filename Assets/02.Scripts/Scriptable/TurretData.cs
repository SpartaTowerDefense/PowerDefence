using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "TurretData", menuName = "CreateData/New Turret Data")]
public class TurretData : ScriptableObject
{
    // 터렛 타입
    [field: SerializeField] public Enums.TurretType Type { get; private set; }

    // 스프라이트
    [field:SerializeField] public Sprite BodyImage { get; private set; }

    // 가격
    [field:SerializeField] public int Price { get; private set; }


    // 배율 설정
    [field: Header("Ratio")]
    [field: Range(0, 5)][field: SerializeField] public float Attack { get; private set; } = 1f;
    [field: Range(0, 5)][field: SerializeField] public float DotDamage { get; private set; } = 1f;
    [field: Range(0, 5)][field: SerializeField] public float Flinch { get; private set; } = 1f;
    [field: Range(0, 5)][field: SerializeField] public float Knockback { get; private set; } = 1f;
    [field: Range(0, 5)][field: SerializeField] public float Coin { get; private set; } = 1f;

    [field: Header("AbilityTimeDuration")]
    [field:Range(0,5)][field: SerializeField] public float Duration { get; private set; } = 1f;

    [field:Header("CannonImg")]
    [field: SerializeField] public Sprite LEVEL0 { get; set; }
    [field: SerializeField] public Sprite LEVEL1 { get; set; }
    [field: SerializeField] public Sprite LEVEL2 { get; set; }

}
