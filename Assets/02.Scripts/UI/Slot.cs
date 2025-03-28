using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] public TurretData TurretData { get; private set; }
    [SerializeField] public Button curButton;
    private UIManager uiManager;
    private Image image;
    private Placement placement;

    private void Start()
    {
        uiManager = UIManager.Instance;
        curButton = GetComponent<Button>();
    }
    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    public void SetData(TurretData data, Placement placement)
    {
        TurretData = data;
        this.placement = placement;
        if (TurretData.BodyImage)
            image.sprite = TurretData.BodyImage;
    }

    public void SetCurData()
    {
        uiManager.Shop.curData = TurretData;
    }
    public void GetMouseUp()
    {
        Debug.Log("Up");
    }

}
