using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Commander commander;
    public Commander Commander { get { return commander; } }
    [SerializeField] private UIDataBinder uIDataBinder;
    public UIDataBinder UIDataBinder { get { return uIDataBinder; } }

    private void Start()
    {
        commander = new Commander(20, 0);
        uIDataBinder.SetUIText();
    }
}
