using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint; // 적 스폰 위치들
    [SerializeField] private SpawnPatternData spawnPattern; //스크랩터블 오브젝트 스테이지 정보 사용

    private int currentSpawnIndex = 0;
    private float timer = 0f;
    private bool gamestart = false;

    void Update()
    {
        if (!gamestart) return;

        if (spawnPattern == null || currentSpawnIndex >= spawnPattern.spawnSequence.Length) return;

        timer += Time.deltaTime;
        var current = spawnPattern.spawnSequence[currentSpawnIndex];

        if (timer >= current.delay)
        {
            SpawnEnemy(current.enemyType);
            timer = 0f;
            currentSpawnIndex++;
        }
    }

    void SpawnEnemy(int type)
    {
        // EnemyFactory를 통해 적 생성
        GameObject enemy = ObjectPoolManager.Instance.GetObject<EnemyFactory>(type);

        // 위치 설정
        enemy.transform.position = spawnPoint.position;
    }

    public void GameStart()
    {
        gamestart = true;
    }
}
