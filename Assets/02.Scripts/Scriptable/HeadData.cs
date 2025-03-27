using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeadData", menuName = "CreateData/new Head Data")]
public class HeadData : ScriptableObject
{
    [field: SerializeField] public int BulletCount { get; set; } = 1;
    [field: Range(0, 5)][field: SerializeField] public float SplashRadtio { get; set; } = 0f;
    [field: SerializeField] public bool CanPenetration { get; set; } = false; // 관통할 수 있는지

}
