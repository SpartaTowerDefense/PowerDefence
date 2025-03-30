using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //수정 부분
    [SerializeField] private Placement placement;
    [SerializeField] private DragHandler dragHandler;


    [SerializeField] public TurretData TurretData { get; private set; }
    [SerializeField] public Button curButton;
    private UIManager uiManager;
    private Image image;

    public Turret curTurret;

    private void Start()
    {
        uiManager = UIManager.Instance;
        curButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void SetData(TurretData data, Placement placement, DragHandler dragHandler)
    {
        TurretData = data;
        this.placement = placement;
        this.dragHandler = dragHandler;
        if (TurretData.BodyImage)
            image.sprite = TurretData.BodyImage;
    }

    public void SetCurDataAndStartDrag(BaseEventData data)
    {
        uiManager.Shop.curData = TurretData;
        dragHandler.OnBeginDrag(data);
    }
   public void GetTankPlace(BaseEventData data)
    {
        dragHandler.EndDrag(data);
    }

}
