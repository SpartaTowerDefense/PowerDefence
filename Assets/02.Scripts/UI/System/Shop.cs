using UnityEngine;
using UnityEngine.Tilemaps;

public class Shop : MonoBehaviour
{
    [Header("카메라")]
    [SerializeField] private Camera mainCam;

    [Header("Object")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] GameObject previewPrefab;

    [Header("UI")]
    [SerializeField] private Transform slotTransform;
    [SerializeField] private GameObject slot;

    [Header("정보")]
    [SerializeField] private TurretData[] bodySo;
    
    private GameObject[] obj;

    public TurretData curData;

    public void Init()
    {
        for (int i = 0; i < obj.Length; i++)
        {
            Slot slotComponent = obj[i].GetComponent<Slot>();
            DragHandler dragHandler = obj[i].GetComponent<DragHandler>();

            dragHandler.Init(mainCanvas, mainCam, UIManager.Instance.Placement, previewPrefab);
            slotComponent.SetData(bodySo[i], UIManager.Instance.Placement, dragHandler);
        }
    }
    private void Awake()
    {
        obj = new GameObject[bodySo.Length];
        for (int i = 0; i < bodySo.Length; i++)
        {
            obj[i] = Instantiate(slot, slotTransform);
        }
    }
}
