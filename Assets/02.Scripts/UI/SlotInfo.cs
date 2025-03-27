using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SlotInfo
{
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] int price;
    [SerializeField] public Sprite icon { get; private set; }

    //public SlotInfo(string name, string description, int price, Image icon)
    //{ 
    //    this.name = name;
    //    this.description = description;
    //    this.price = price;
    //    this.icon = icon;
    //}

    public Sprite SetIcon()
    {
        return icon;
    }
}
