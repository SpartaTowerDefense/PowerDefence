using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention
{
    public static T GetComponentInChildrenEX<T>(this Transform transform, string gameObjectName) where T : Component
    {
        return Utils.GetComponentInChildren<T>(transform, gameObjectName);
    }
}
