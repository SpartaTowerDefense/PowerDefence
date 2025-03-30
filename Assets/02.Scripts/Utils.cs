using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Utils
{
    // 자주 사용은 x
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

    public static Collider2D[] OverlapCircleAllSorted(Vector2 center, float radius, int layerMask, Vector2 thisPoint)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(center, radius, layerMask);

        return colliders
            .OrderBy(c => Vector2.SqrMagnitude((Vector2)c.transform.position - thisPoint))
            .ToArray();
    }

}
