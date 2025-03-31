using UnityEngine;
using UnityEngine.Tilemaps;

public class Shop : MonoBehaviour
{
    [Header("카메라")]
    [SerializeField] private Camera mainCam;

    [Header("Object")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Tilemap roadTile;
    [SerializeField] private Tilemap groundTile;
    [SerializeField] GameObject previewPrefab;

    [SerializeField] private TurretData[] bodySo;
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    public TurretData curData;

    [Header("배치시스템")]
    [SerializeField] Placement placement;
    private void Start()
    {
        for (int i = 0; i < bodySo.Length; i++)
        {
            GameObject obj = Instantiate(slot, slotTransform);

            Slot slotComponent = obj.GetComponent<Slot>();
            DragHandler dragHandler = obj.GetComponent<DragHandler>();

            dragHandler.Init(mainCanvas, mainCam, placement, roadTile, groundTile, previewPrefab);
            slotComponent.SetData(bodySo[i], placement, dragHandler);
        }
    }

    public void BuyTurret()
    {
        if (!curData) return;
        if (curData.Price > UIManager.Instance.Commander.gold) return;
        UIManager.Instance.Commander.SubtractGold(curData.Price);
    }
}
