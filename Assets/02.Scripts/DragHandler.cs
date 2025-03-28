using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class DragHandler : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Canvas canvas; // 마우스의 좌표를 UI로 변환할 때 필요한 변수
    [SerializeField] private Placement placement;
    [SerializeField] private GameObject tankPreviewPrefab;
    [SerializeField] private Camera maincam;
    [SerializeField] private Tilemap roadTile;


    private RectTransform rectTransform; // UI 오브젝트를 마우스의 좌표로 움직이게 만들기 위한 변수
    private Vector2 originalPosition; // UI상 초기 위치
    private GameObject previewInstance;
    private SpriteRenderer previewRenderer;

    private Coroutine dragCoroutine;
    private bool isDrag = false;
    private bool isPlace = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition; //UI 아이콘의 초기 위치를 기준 앵커포인트로부터의 상대 위치로 저장
    }
    public void Init(Canvas canvas, Camera mainCam, Placement placement, Tilemap roadTile, GameObject previewPrefab)
    {
        this.canvas = canvas;
        this.maincam = mainCam;
        this.placement = placement;
        this.roadTile = roadTile;
        this.tankPreviewPrefab = previewPrefab;
    }
    public void OnBeginDrag(BaseEventData data)
    {
        if (isPlace || UIManager.Instance.Shop.curData == null) return;

        isDrag = true; //드래그상태
        previewInstance = Instantiate(tankPreviewPrefab);
        previewRenderer = previewInstance.GetComponent<SpriteRenderer>();
        previewRenderer.sprite = UIManager.Instance.Shop.curData.BodyImage;

        dragCoroutine = StartCoroutine(HandleDragPreview());
    }

    public void OnDrag(BaseEventData data)
    {

    }

    public void EndDrag(BaseEventData data)
    {
        isDrag = false; //드래그상태 해제
        StopCoroutine(dragCoroutine);

        PointerEventData ped = data as PointerEventData;
        Vector3 worldPos = maincam.ScreenToWorldPoint(ped.position);
        worldPos.z = 0f;

        TurretData selectedData = UIManager.Instance.Shop.curData;

        bool isSuccess = placement.TryPlaceTank(worldPos, selectedData);
        Destroy(previewInstance);

        if (isSuccess)
        {
            isPlace = true;
            gameObject.SetActive(true);
        }
        else
        {
            rectTransform.anchoredPosition = originalPosition; //드래그 종료후 오브젝트를 배치하지 못했다면
                                                               // UI의 최초 위치로 
        }
    }

    private IEnumerator HandleDragPreview()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        while (isDrag)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = maincam.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            Vector3Int cellPos = roadTile.WorldToCell(worldPos);
            Vector3 snappedPos = roadTile.CellToWorld(cellPos);
            previewInstance.transform.position = snappedPos;

            bool canPlace = placement.CanPlaceTank(cellPos);

            if (canPlace)
            {
                previewRenderer.color = new Color(0f, 1f, 0f, 0.5f);
            }
            else
            {
                previewRenderer.color = new Color(1f, 0f, 0f, 0.5f);
            }
            bool isOverUI = IsPointerOverUI();
            previewInstance.gameObject.SetActive(!isOverUI);

            yield return null;
        }
    }
    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new PointerEventData(EventSystem .current);

        eventData.position = Mouse.current.position.ReadValue();

        var results = new List<RaycastResult>();
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        raycaster.Raycast(eventData, results);

        return results.Count > 0;
    }

    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }
}
    //private void OnLeftClickStarted(InputAction.CallbackContext context) //드래그시작시
    //{
    //    if (isPlace) return;
    //    isDrag = true;
    //}

    //private void OnLeftClickCanceled(InputAction.CallbackContext context) //드래그 종료시
    //{
    //    if (!isDrag || isPlace) return; //
            
    //    isDrag = false;

    //    TurretData selectedData = UIManager.Instance.Shop.curData;

    //    if(selectedData == null)
    //    {
    //        rectTransform.anchoredPosition = originalPosition;
    //    }

    //    Vector2 mouseScreenPos = inputActions.PlayerActions.MousePosition.ReadValue<Vector2>();
    //    Vector3 worldPos = Camera.main.ScreenToWorldPoint(
    //        new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)
    //    ); 
    //    worldPos.z = 0f; //z축은 고정

    //    bool isSuccess = placement.TryPlaceTank(worldPos, selectedData);
    //    if (isSuccess)
    //    {
    //        isPlace = true;
    //        gameObject.SetActive(true);
    //    }
    //    else
    //    {
    //        rectTransform.anchoredPosition = originalPosition; //드래그 종료후 오브젝트를 배치하지 못했다면
    //                                                           // UI의 최초 위치로 
    //    }
    //}


