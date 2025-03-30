using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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

    public Turret curTurret;

    private void Start()
    {
        commander = new Commander(20, 0);
        DOTween.Init(true, true);
        uiDataBinder.Init();
        OnActive(true, title.gameObject.transform.parent.gameObject);
    }

    public void OnActive(bool set, GameObject a = null, GameObject b = null, GameObject c = null, GameObject d = null)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.TryGetComponent<ButtonEffect>(out ButtonEffect bf))
            {
                continue;
            }
            else if (child == a || child == b || child == c || child == d)
            {
                child.SetActive(set);
            }
            else
            {
                child.SetActive(!set);
            }
        }
    }
}
