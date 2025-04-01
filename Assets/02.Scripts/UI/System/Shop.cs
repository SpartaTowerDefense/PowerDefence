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

    [Header("UI")]
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    [Header("정보")]
    [SerializeField] private TurretData[] bodySo;
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

    public bool BuyTurret()
    {
        if (!curData) return false;
        if (!CanBuy(curData)) return false ;
        GameManager.Instance.commander.SubtractGold(curData.Price); // 선택된 터렛의 정보를 넘겨야됨 @bsy
        UIManager.Instance.UIDataBinder.SetUIText();
        return true;
    }
    public bool CanBuy(TurretData data) //살 수 있는지 확인하고 반환해주는 메서드
    {
        return data && GameManager.Instance.commander.gold >= data.Price;
    }
}
