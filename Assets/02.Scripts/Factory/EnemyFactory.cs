using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : FactoryBase
{
    private readonly string PATH = $"Scriptable/EnemySo/TestEnemy/";
    // private readonly string PATH = $"Scriptable/EnemySo/"; //실제 동작시 사용

    // 타입별 이름
    private const string Normal = nameof(Normal);
    private const string Burning = nameof(Burning);
    private const string Freeze = nameof(Freeze);
    private const string Small = nameof(Small);
    private const string Speed = nameof(Speed);
    private const string Boss = nameof(Boss);

    private List<EnemyData> enemyDataList = new();

    [SerializeField] private List<WaypointPath> stagePaths = new();
    public WaypointPath path;
    [SerializeField] public EnemySpawner enemySpawner;

    public void Gamestart()
    {
        enemySpawner.GameStart();
    }

    public void SetPathByStage(int stage)
    {
        if (stage >= 0 && stage < stagePaths.Count)
        {
            path = stagePaths[stage];
        }
        else
        {
            Debug.LogWarning($"EnemyFactory: 유효하지 않은 스테이지 인덱스 ({stage})입니다.");
        }
    }

    private void Awake()
    {
        // 팩토리 매니저에 등록 (선택 사항)
        if(!FactoryManager.Instance.path.ContainsKey(typeof(EnemyFactory).Name))
            FactoryManager.Instance.path.Add(typeof(EnemyFactory).Name, this);

        // EnemyData ScriptableObject 로드
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Normal, $"{PATH}{Normal}"));
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Burning, $"{PATH}{Burning}"));
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Freeze, $"{PATH}{Freeze}"));
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Small, $"{PATH}{Small}"));
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Speed, $"{PATH}{Speed}"));
        enemyDataList.Add(ResourceManager.Instance.LoadResource<EnemyData>(Boss, $"{PATH}{Boss}"));
    }

    public override GameObject CreateObject(GameObject obj = null, int enumType = -1)
    {
        if (enumType < 0 || enumType >= enemyDataList.Count)
        {
            Debug.LogError("EnemyFactory: 잘못된 enumType입니다.");
            return null;
        }

        EnemyData data = enemyDataList[enumType];

        if (obj == null)
            obj = Instantiate(Prefab, transform); // 기본 프리팹에서 생성

        Enemy enemy = obj.GetComponent<Enemy>();
        if (enemy == null)
        {
            Debug.LogError("EnemyFactory: Enemy 컴포넌트가 없습니다.");
            return obj;
        }

        enemy.enemyData = data;
        enemy.transform.position = enemySpawner.spawnPoint.position;
        enemy.InitializeFromData(); // 초기화 호출 (Start에서 자동으로 하게 해도 OK)

        return obj;
    }
}
