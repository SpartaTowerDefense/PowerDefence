using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class DragHandler : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Canvas canvas; // 마우스의 좌표를 UI로 변환할 때 필요한 변수
    public Placement placement;
    [SerializeField] private GameObject tankPreviewPrefab;
    [SerializeField] private Camera maincam;
    [SerializeField] private Tilemap roadTile;
    [SerializeField] private Tilemap groundTile;

    private GameObject previewInstance;

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

        var controller = previewInstance.GetComponent<PreviewTurretController>();
        if(controller != null)
        {
            controller.SetBodySprite(UIManager.Instance.Shop.curData.BodyImage);
        }

        dragCoroutine = StartCoroutine(HandleDragPreview(controller));
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

    private IEnumerator HandleDragPreview(PreviewTurretController controller) //원래 비어있었음.
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
            previewInstance.transform.rotation = placement.GetCurrentRotation();
            
            bool canPlace = placement.CanPlaceTank(cellPos);
            //추가된 내용
            controller?.SetPlacementColor(canPlace);

            bool isOverUI = IsPointerOverUI();
            previewInstance.gameObject.SetActive(!isOverUI);

            yield return null;
        }
    }
    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

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


