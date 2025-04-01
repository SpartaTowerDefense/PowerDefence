using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    // 원본 프리펩을 가지고 있는 딕셔너리
    private Dictionary<string, UnityEngine.Object> original = new();
    //private Dictionary<string, List<UnityEngine.Object>> GroupResource = new();

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

    public int LoadResourceAll<T>(string key, string pathFolder) where T : UnityEngine.Object
    {
        if (pathFolder == null)
        {
            Debug.LogWarning($"dont find {key}");
            return 0;
        }

        // 새로 불러와야됩니다.
        var objs = Resources.LoadAll(pathFolder);

        for(int i = 0; i<objs.Length; i++)
        {
            var obj = objs[i];
            if (obj is T)
            {
                T result = obj as T;
                if (!original.ContainsKey($"{key}{i}"))
                    original.Add($"{key}{i}", result);
                else
                    original[$"{key}{i}"] = result;
            }
        }

        return objs.Length;
    }
}
