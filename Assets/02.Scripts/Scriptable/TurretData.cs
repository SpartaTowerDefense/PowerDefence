﻿using System.Collections;
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

    // 포씬 디폴트 값
    [field:Header("Cannon Datas")]
    [field: SerializeField] public int BulletCount { get; set; } = 1;
    [field: Range(0, 5)][field: SerializeField] public float SplashRatio { get; set; } = 0f;
    [field: SerializeField] public bool CanPenetration { get; set; } = false; // 관통할 수 있는지

}
