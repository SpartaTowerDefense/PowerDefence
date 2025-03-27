using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryManager : Singleton<FactoryManager>
{
    public Dictionary<string, FactoryBase> path = new();


    // 딕셔너리 초기화
    public void ClearPath()
    {
        path.Clear();
    }
}
