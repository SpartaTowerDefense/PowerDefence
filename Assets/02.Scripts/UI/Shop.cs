using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private BodyData[] bodySo;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    private UIManager uiManager;
    public BodyData curData;

    private void Start()
    {
        uiManager = UIManager.Instance;
        for (int i = 0; i < bodySo.Length; i++)
        {
            Slot obj = Instantiate(slot, slotTransform).GetComponent<Slot>();
            obj.SetData(bodySo[i]);
        }
    }

    void BuyTurret()
    {
        if (!curData) return;
        if (curData.Price > uiManager.Commander.gold) return;
        uiManager.Commander.SubtractGold(curData.Price);
    }
}
