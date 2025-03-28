using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : FactoryBase
{
    private void Awake()
    {
        FactoryManager.Instance.path.Add(typeof(BulletFactory).Name, this);
    }
    public override GameObject CreateObject(GameObject obj = null, int enumType = -1)
    {
        if (enumType == -1)
        {
            Debug.Log("OutRange Array");
            return null;
        }

        // 매개변수로 받은 오브젝트 체킹
        if (obj == null)
            obj = Instantiate(Prefab);
       
        return obj;
    }
}
