using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private DragHandler dragHandler;
    public Turret curTurret;

    [SerializeField] public TurretData TurretData { get; private set; }
    [SerializeField] public Button curButton;
    private Image image;

    private void Start()
    {
        curButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void SetData(TurretData data, Placement placement, DragHandler dragHandler)
    {
        TurretData = data;
        //UIManager.Instance.Placement = placement;
        this.dragHandler = dragHandler;
        if (TurretData.BodyImage)
            image.sprite = TurretData.BodyImage;
    }

    public void SetCurDataAndStartDrag(BaseEventData data)
    {
        //UIManager.Instance.Shop.curData = TurretData;
        dragHandler.OnBeginDrag(data);
    }
   public void GetTurretPlace(BaseEventData data)
    {
        dragHandler.EndDrag(data);
    }

}
