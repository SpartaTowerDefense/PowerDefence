using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BodyData", menuName = "CreateData/New Body Data")]
public class BodyData : ScriptableObject
{
    // 스프라이트
    [field:SerializeField] public Sprite SpriteImage { get; private set; }
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

}
