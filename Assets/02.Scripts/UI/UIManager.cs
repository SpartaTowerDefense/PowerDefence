using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    GameObject uiCanvas;
    GameObject mainCanvas;

    protected override void Awake()
    {
        base.Awake();
        uiCanvas = Resources.Load<GameObject>("UI");
        mainCanvas = Instantiate(uiCanvas);
        DontDestroyOnLoad(mainCanvas);
        InjectReferences(mainCanvas);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        //Placement = obj.GetComponentInChildren<Placement>(true);
        Placement = obj.transform.GetComponentInChildrenEX<Placement>("SlotMask");
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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Title.gameObject.SetActive(true);
        EndPanel.SetActive(false);

    }
}
