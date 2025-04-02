using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] private List<GameObject> alwaysActiveObjects;

    public void Init()
    {
        ActiveCnavasChild(true, UIManager.Instance.Title.gameObject, UIManager.Instance.Shop.gameObject);
    }

    public void ActiveCnavasChild(bool enable, params GameObject[] onActive)
    {
        for (int i = 0; i < UIManager.Instance.MainCanvas.transform.childCount; i++)
        {
            GameObject child = UIManager.Instance.MainCanvas.transform.GetChild(i).gameObject;

            if (alwaysActiveObjects.Contains(child))
            {
                continue;
            }
            else if (onActive.Where(n => n != null).Any(n => n == child))
            {
                child.SetActive(enable);
            }
            else
            {
                child.SetActive(!enable);
            }
        }
    }
}

