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

    // 살아있는 적 리스트
    private List<Enemy> aliveEnemies = new();
    public int AliveEnemyCount => aliveEnemies.Count;

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
        GameObject obj = ObjectPoolManager.Instance.GetObject<EnemyFactory>(type);

        if (obj.TryGetComponent(out Enemy enemy))
        {
            enemy.transform.position = spawnPoint.position;

            // 죽음 콜백 등록
            enemy.OnDeath += HandleEnemyDeath;

            // 리스트에 추가
            aliveEnemies.Add(enemy);
            UIManager.Instance.UIDataBinder.SetUIText();
        }
        else
        {
            Debug.LogError("EnemySpawner: Enemy 컴포넌트가 존재하지 않습니다.");
        }
    }

    private void HandleEnemyDeath(Enemy deadEnemy)
    {
        if (aliveEnemies.Contains(deadEnemy))
        {
            aliveEnemies.Remove(deadEnemy);
            Debug.Log($"적 사망: 남은 적 {aliveEnemies.Count}");
            UIManager.Instance.UIDataBinder.SetUIText();
            // 모두 죽었는지 확인
            if (aliveEnemies.Count == 0 && currentSpawnIndex >= spawnPattern.spawnSequence.Length)
            {
                Debug.Log("이번 웨이브 모든 적 제거 완료!");
                GameManager.Instance.StageClear();
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        gamestart = true;
        currentSpawnIndex = 0;
        timer = 0f;
        aliveEnemies.Clear();
    }
}
