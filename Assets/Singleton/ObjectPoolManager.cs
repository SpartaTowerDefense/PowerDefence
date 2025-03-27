using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    // 게임오브젝트를 저장할 수 있게 저장할 수 있는 공간 필요
    // 키로 반환해줄 게임오브젝트 타입을 적고, 벨류값은 리스트로 하여서 큐에 사용할 수 있는 게임오브젝트가 있으면 반환
    // 없으면 새로 생성후 큐에 넣어준뒤, 반환

    // 씬을 넘어가면 모두 초기화 << 나중에 만들어 봐야겠다

    private Dictionary<string, Queue<GameObject>> objectPool = new();

    private FactoryManager factory;

    protected override void Awake()
    {
        base.Awake();
        factory = FactoryManager.Instance;
    }

    /// <summary>
    /// T : FactoryType, key : FactoryTypeName
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public GameObject GetObject<T>() where T : FactoryBase
    {
        GameObject poolGo = null;
        if(objectPool.TryGetValue(typeof(T).Name, out var queue))
        {
            // 게임오브젝트가 들어 있을때
            if(queue.Count > 0)
            {
                GameObject dequeueGo = queue.Dequeue();
                poolGo = dequeueGo;
            }
        }

        // 해당되는 팩토리 생성 및 추가
        GameObject obj = factory.path[typeof(T).Name].CreateObject(poolGo);

        // 해당되는 키가 없으면 새로 생성
        if (!objectPool.ContainsKey(typeof(T).Name))
            objectPool.Add(typeof(T).Name, new Queue<GameObject>());

        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// T : FactoryType, GameObject : FactoryType으로 만들어진 obj
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public void ReturnObject<T>(GameObject obj)
    {
        if (obj == null)
            return;

        obj.SetActive(false);
        objectPool[typeof(T).Name].Enqueue(obj);
    }

    public void ClearObjectPool()
    {
        objectPool.Clear();
    }

}
