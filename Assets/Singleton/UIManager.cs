using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class UIManager : Singleton<UIManager>
{
    // 새로 만들어진 객체를 저장할 수 있는 딕셔너리
    private Dictionary<string, UIPopupBase> popupDict = new();

    public T GetPopup<T>() where T : UIPopupBase
    {
        if(popupDict.TryGetValue(typeof(T).Name, out var value))
        {
            return value as T;
        }

        // 원본 프리펩을 가져와야한다.
        // 새로 생성하고, 딕셔너리에 저장

        string name = typeof(T).Name;
        string path = $"Popup\\{name}";

        GameObject prefab = ResourceManager.Instance.LoadResource<GameObject>(name, path);
        GameObject go = Instantiate(prefab);

        if(go.TryGetComponent(out T popupScript))
        {
            popupDict[typeof(T).Name] =  popupScript;

            return popupScript;
        }
        else
        {
            Debug.Log($"didn't make {typeof(T).Name} object");
            return null;
        }
    }

    public void ClearPopup()
    {
        popupDict.Clear();
    }
}
