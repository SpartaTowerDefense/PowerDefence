using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [field: SerializeField] public Canvas MainCanvas { get; private set; }
    [field: SerializeField] public UIDataBinder UIDataBinder { get; private set; }
    [field: SerializeField] public Shop Shop { get; private set; }
    [field: SerializeField] public UIButtonHandler UIButtonHandler { get; private set; }
    [field: SerializeField] public Title Title { get; private set; }
    [field: SerializeField] public ButtonEffect ButtonEffect { get; private set; }
    [field: SerializeField] public Placement Placement { get; set; }




    [SerializeField] private List<GameObject> alwaysActiveObjects;
    public Turret curTurret;

    private void Start()
    {
        ActiveCnavasChild(true, Title.gameObject.transform.parent.gameObject, Shop.gameObject);
        Init();
    }

    private void Init()
    {
        DOTween.Init(true, true);
        UIDataBinder.Init();
        Shop.Init();
    }

    public void ActiveCnavasChild(bool enable, params GameObject[] onActive)
    {
        for (int i = 0; i < MainCanvas.transform.childCount; i++)
        {
            GameObject child = MainCanvas.transform.GetChild(i).gameObject;

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
