using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Commander commander;
    public Commander Commander { get { return commander; } }
    [SerializeField] private UIDataBinder uIDataBinder;
    public UIDataBinder UIDataBinder { get { return uIDataBinder; } }
    [SerializeField] private Shop shop;
    public Shop Shop { get => shop; set => shop = value; }
    [SerializeField] private UIButtonHandler uiButtonHandler;
    public UIButtonHandler UIButtonHandler { get { return uiButtonHandler; } }

    private void Start()
    {
        commander = new Commander(20, 0);
        uIDataBinder.Init();
        uIDataBinder.SetUIText();
    }
}
