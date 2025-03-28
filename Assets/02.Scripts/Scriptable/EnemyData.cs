using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "CreateData/New Enemy Data")]
public class EnemyData : ScriptableObject
{
    // 스프라이트
    // [field: SerializeField] public Sprite SpriteImage { get; private set; }

    // 배율 설정
    [field: Header("Status")]
    [field: Range(0, 50)][field: SerializeField] public float Health { get; private set; } = 5f;
    [field: Range(0, 5)][field: SerializeField] public float MovementSpeed { get; private set; } = 1f;
    [field: Range(0, 5)][field: SerializeField] public float FrozeTime { get; private set; } = 1f;
    [field: Range(0, 1)][field: SerializeField] public float KnockbackTime { get; private set; } = 0.3f;
    [field: Range(0, 5)][field: SerializeField] public float knockbackDistance { get; private set; } = 1f;
    [field: Range(0, 1)][field: SerializeField] public float BurningTime { get; private set; } = 5f;
    [field: Range(0, 5)][field: SerializeField] public float Flammable { get; private set; } = 0.5f;

    [field: Header("Reward")]
    [field: Range(0, 10)][field: SerializeField] public float RewardCoin { get; private set; } = 5f;
}
