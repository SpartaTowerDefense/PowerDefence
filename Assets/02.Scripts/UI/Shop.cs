using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private TurretData[] bodySo;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    public TurretData curData;

    [SerializeField] Placement placement;
    private void Start()
    {
        for (int i = 0; i < bodySo.Length; i++)
        {
            Slot obj = Instantiate(slot, slotTransform).GetComponent<Slot>();
            obj.SetData(bodySo[i], placement);
        }
    }

    void BuyTurret()
    {
        if (!curData) return;
        if (curData.Price > UIManager.Instance.Commander.gold) return;
    }
}
