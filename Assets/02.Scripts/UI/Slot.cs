using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    //[SerializeField] public BodyData bodyData { get; private set; }
    [SerializeField] public Button curButton;
    private UIManager uiManager;
    private Image image;
    private Placement placement;

    private void Start()
    {
        uiManager = UIManager.Instance;
        curButton = GetComponent<Button>();
        //curButton.onClick.AddListener(SetCurData);
    }
    private void OnEnable()
    {
        image = GetComponent<Image>();
    }

    //public void SetData(BodyData data, Placement placement)
    //{
    //    bodyData = data;
    //    this.placement = placement;
    //    if (bodyData.SpriteImage)
    //        image.sprite = bodyData.SpriteImage;
    //}

    //public void SetCurData()
    //{
    //    uiManager.Shop.curData = bodyData;
    //}
    public void GetMouseUp()
    {
        Debug.Log("Up");
    }

}
