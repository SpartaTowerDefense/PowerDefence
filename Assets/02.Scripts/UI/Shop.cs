using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private SlotInfo[] slotInfo;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;


    private void Start()
    {
        for(int i = 0; i < slotInfo.Length; i++)
        {
            Slot obj = Instantiate(slot, slotTransform).GetComponent<Slot>();
            obj.SlotInfo = slotInfo[i];
            obj.SetData();
        }
    }

}
