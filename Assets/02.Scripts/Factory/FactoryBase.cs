using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FactoryBase : MonoBehaviour
{
    // 팩토리 템플릿

    public GameObject Prefab;
    protected ResourceManager resourceManager;

    public abstract GameObject CreateObject(GameObject obj = null);
}
