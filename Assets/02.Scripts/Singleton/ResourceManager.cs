using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    // 원본 프리펩을 가지고 있는 딕셔너리
    private Dictionary<string, UnityEngine.Object> original = new();

    public T LoadResource<T>(string key, string path = null) where T : UnityEngine.Object
    {
        if(original.TryGetValue(key, out var value))
        {
            return value as T;
        }

        if(path == null)
        {
            Debug.LogWarning($"dont find {key}");
            return null;
        }

        // 새로 불러와야됩니다.
        var obj = Resources.Load(path);

        if(obj is T)
        {
            T result = obj as T;
            original[key] = result;
            return result;
        }

        Debug.LogWarning($"not found {path}");
        return null;
    }
}
