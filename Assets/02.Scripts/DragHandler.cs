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
    [SerializeField] private Tilemap groundTile;

    private GameObject previewInstance;
    private SpriteRenderer previewRenderer;
    private SpriteRenderer outlineRenderer;
    private GameObject outlineInstance;

    private Coroutine dragCoroutine;
    private bool isDrag = false;
    private bool isPlace = false;

    public void Init(Canvas canvas, Camera mainCam, Placement placement, Tilemap roadTile, Tilemap groundTile, GameObject previewPrefab)
    {
        this.canvas = canvas;
        this.maincam = mainCam;
        this.placement = placement;
        this.roadTile = roadTile;
        this.groundTile = groundTile;
        this.tankPreviewPrefab = previewPrefab;
    }
    public void OnBeginDrag(BaseEventData data)
    {
        if (UIManager.Instance.Shop.curData == null) return;

        isDrag = true; //드래그상태
        previewInstance = Instantiate(tankPreviewPrefab);
        previewRenderer = previewInstance.GetComponent<SpriteRenderer>();
        //outlineRenderer = outlineInstance.GetComponentInChildren<SpriteRenderer>();   
        previewRenderer.sprite = UIManager.Instance.Shop.curData.BodyImage;
        //outlineRenderer.sprite = UIManager.Instance.Shop.curData.BodyImage;

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
    }

    private IEnumerator HandleDragPreview()
    {
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();

        while (isDrag)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            Vector3 worldPos = maincam.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            Vector3Int cellPos = groundTile.WorldToCell(worldPos);
            Vector3 snappedPos = groundTile.CellToWorld(cellPos);
            snappedPos += new Vector3(0.5f, 0.5f, 0);
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


