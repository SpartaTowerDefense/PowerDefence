using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    // ���� ����� x
    public static T GetComponentInChildren<T>(Transform transform, string gameObjectName) where T : Component
    {
        T[] components = transform.GetComponentsInChildren<T>(true);

        foreach (var item in components)
        {
            if(item.name.Equals(gameObjectName))
                return item;
        }

        return null;
    }
}
