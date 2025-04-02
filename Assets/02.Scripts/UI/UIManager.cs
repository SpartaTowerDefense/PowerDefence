using DG.Tweening;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public UICanvas MainCanvas { get; private set; }
    public UIDataBinder UIDataBinder { get; private set; }
    public Shop Shop { get; private set; }
    public UIButtonHandler UIButtonHandler { get; private set; }
    public Title Title { get; private set; }
    public ButtonEffect ButtonEffect { get; private set; }
    public Placement Placement { get; set; }
    public SelectTurretUI SelectTurretUI { get; private set; }
    public GameObject EndPanel { get; private set; }

    public Turret curTurret;

    protected override void Awake()
    {
        base.Awake();
        GameObject uiCanvas = Resources.Load<GameObject>("UI");
        GameObject mainCanvas = Instantiate(uiCanvas);
        InjectReferences(mainCanvas);
    }

    private void Start()
    {
        Init();
    }

    public void InjectReferences(GameObject obj)
    {

        MainCanvas = obj.GetComponentInChildren<UICanvas>();
        UIDataBinder = obj.GetComponentInChildren<UIDataBinder>(true);
        Shop = obj.GetComponentInChildren<Shop>();
        UIButtonHandler = obj.GetComponentInChildren<UIButtonHandler>(true);
        Title = obj.GetComponentInChildren<Title>(true);
        ButtonEffect = obj.GetComponentInChildren<ButtonEffect>(true);
        Placement = obj.GetComponentInChildren<Placement>(true);
        EndPanel = obj.GetComponentInChildren<EndPanel>(true).gameObject;
        SelectTurretUI = obj.GetComponentInChildren<SelectTurretUI>(true);
        SelectTurretUI._camera = GameObject.Find("PreviewCam")?.GetComponent<Camera>();
    }

    private void Init()
    {
        DOTween.Init(true, true);
        UIDataBinder.Init();
        MainCanvas.Init();
        Shop.Init();
    }
}
