using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private BodyData[] bodySo;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    public BodyData curData;

    private void Start()
    {
        for(int i = 0; i < bodySo.Length; i++)
        {
            Slot obj = Instantiate(slot, slotTransform).GetComponent<Slot>();
            obj.SetData(bodySo[i]);
        }
    }

    void BuyTurret()
    {
        if (!curData) return;
        if (curData.Price > UIManager.Instance.Commander.gold) return;
    }
}
