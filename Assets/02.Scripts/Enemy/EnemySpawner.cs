using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints; // 적 스폰 위치들
    [SerializeField] private float spawnInterval = 200000f; // 스폰 간격
    [SerializeField] private int enemyType = 0; // 어떤 EnemyData 쓸지

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnEnemy(enemyType);
            enemyType += 1;
            if (enemyType > 4)
            {
                enemyType = 0;
            }
            timer = 0;
        }
    }

    void SpawnEnemy(int type)
    {
        // 스폰 위치 무작위 선택
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // EnemyFactory를 통해 적 생성
        GameObject enemy = ObjectPoolManager.Instance.GetObject<EnemyFactory>(type);

        // 위치 설정
        enemy.transform.position = spawnPoint.position;
    }
}
