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

    private static Collider2D[] buffer = new Collider2D[10];

    public static Collider2D[] OverlapCircleAllSorted(Vector2 center, float radius, int layerMask, Vector2 referencePoint)
    {
        int count = Physics2D.OverlapCircleNonAlloc(center, radius, buffer, layerMask);

        return buffer
            .Take(count) // 사용된 요소까지만 정렬
            .OrderBy(c => Vector2.SqrMagnitude((Vector2)c.transform.position - referencePoint))
            .ToArray();
    }

}
