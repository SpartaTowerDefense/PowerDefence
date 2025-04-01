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
    private Commander commander;
    public Commander Commander { get => commander; }
    [SerializeField] private UIDataBinder uiDataBinder;
    public UIDataBinder UIDataBinder { get => uiDataBinder; }
    [SerializeField] private Shop shop;
    public Shop Shop { get => shop; set => shop = value; }
    [SerializeField] private UIButtonHandler uiButtonHandler;
    public UIButtonHandler UIButtonHandler { get => uiButtonHandler; }
    [SerializeField] private Title title;
    public Title Title { get => title; }
    [SerializeField] private ButtonEffect buttonEffect;
    public ButtonEffect ButtonEffect { get => buttonEffect; }
    [SerializeField] private Placement placement;
    public Placement Placement { get => placement; set => placement = value; }

    [SerializeField] private List<GameObject> alwaysActiveObjects;

    public Turret curTurret;

    private void Start()
    {
        commander = new Commander(20, 0);
        DOTween.Init(true, true);
        uiDataBinder.Init();
        ActiveCnavasChild(true, title.gameObject.transform.parent.gameObject,shop.gameObject);
    }

    public void ActiveCnavasChild(bool enable, params GameObject[] onActive)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

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
