using UnityEngine;

[CreateAssetMenu(fileName = "SpawnPatternData", menuName = "Stage/SpawnPattern")]
public class SpawnPatternData : ScriptableObject
{
    public SpawnInfo[] spawnSequence;
}

[System.Serializable]
public class SpawnInfo
{
    public int enemyType;     // EnemyData 타입 인덱스
    public float delay;       // 이 적을 소환하기 전 대기 시간
}
