using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField]private SlotInfo slotInfo;
    public SlotInfo SlotInfo { get { return slotInfo; } set => slotInfo = value; }
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void SetData()
    {
        if(slotInfo.icon != null)
            image.sprite = slotInfo.SetIcon();
    }
}
