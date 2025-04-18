using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class DragHandler : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Canvas canvas; // 마우스의 좌표를 UI로 변환할 때 사용하는 캔버스 

    [SerializeField] private GameObject tankPreviewPrefab;  // 
    [SerializeField] private Camera maincam; // 스크린 좌표에서 월드 좌표로 변환하는 메인카메라

    private GameObject previewInstance; //드래그할 때 보이는 미리보기 

    private Coroutine dragCoroutine;
    private bool isDrag = false; // 현재 드래그상태인지

    /// <summary>
    /// 외부에서 초기화 (Shop에서 초기화)
    /// </summary>
    public void Init(Canvas canvas, Camera mainCam, Placement placement, GameObject previewPrefab)
    {
        this.canvas = canvas;
        this.maincam = mainCam;
        UIManager.Instance.Placement = placement;
        this.tankPreviewPrefab = previewPrefab;
    }

    /// <summary>
    /// 드래그 시작 시 호출
    /// </summary>
    public void OnBeginDrag(BaseEventData data)
    {

        //if (UIManager.Instance.Shop.curData == null) return; //현재선택한 정보가 없다면 반환

        isDrag = true; //드래그 상태
        previewInstance = Instantiate(tankPreviewPrefab); //미리보기 프리펩 생성

        var controller = previewInstance.GetComponent<PreviewTurretController>();
        if(controller != null)
        {
            controller.SetBodySprite(UIManager.Instance.Shop.curData.BodyImage); //선택한 Turret의 Body로 입히기
        }

        dragCoroutine = StartCoroutine(HandleDragPreview(controller)); //드래그를 통해 계속 미리보기 인스턴스를 따라가도록 만든 코루틴
    }

    public void OnDrag(BaseEventData data)
    {

    }

    /// <summary>
    /// 드래그 종료 시 호출
    /// </summary>
    public void EndDrag(BaseEventData data)
    {
        isDrag = false; //드래그상태 해제

        if (IsPointerOverUI())
        {
            if (previewInstance != null)
                Destroy(previewInstance);

            return; 
        }

        PointerEventData ped = data as PointerEventData; // Unity 이벤트 시스템을 활용하여 마우스의 정보가 담긴 데이터로 변환과정
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(ped.position);
        worldPos.z = 0f;

        TurretData selectedData = UIManager.Instance.Shop.curData;

        if (!GameManager.Instance.commander.CanBuy(selectedData.Price)) //구매가 불가능하다면
        {
            Destroy(previewInstance); //미리보기 프리펩 삭제
            return;
        }
        bool isSuccess = UIManager.Instance.Placement.TryPlaceTurret(worldPos, selectedData); //worldPos에 원하는 Data의 Turret을 배치하기 위한 bool변수
        Destroy(previewInstance);  //미리보기 프리펩은 삭제

        if (isSuccess)
        {
            GameManager.Instance.commander.SubtractGold(selectedData.Price);
            UIManager.Instance.UIDataBinder.SetUIText();
        }

    }

    /// <summary>
    /// 드래그 중 프리뷰터렛 마우스를 따라가게 하고, 배치 가능 여부 색상 갱신
    /// </summary>
    private IEnumerator HandleDragPreview(PreviewTurretController controller) 
    {
        while (isDrag)
        {
            Vector2 mousePos = Pointer.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f;

            Vector3Int cellPos = UIManager.Instance.Placement.WorldToCell(worldPos); //마우스의 가장 가까운 Cell좌표를 계산 (int형으로 만들기)
            Vector3 snappedPos = UIManager.Instance.Placement.SnapToCenter(cellPos); // 셀의 중심에 오도록 만들기 위한 좌표설정

            previewInstance.transform.position = snappedPos;
            previewInstance.transform.rotation = UIManager.Instance.Placement.GetCurrentRotation(); // 회전값 적용
            
            bool canPlace = UIManager.Instance.Placement.CanPlaceTurret(cellPos); //배치 가능한지 확인
            controller?.SetPlacementColor(canPlace); //가능하다면 색깔 변경

            bool isOverUI = IsPointerOverUI(); //마우스가 UI에 존재한다면 미리보기 프리펩 숨기기
            previewInstance.gameObject.SetActive(!isOverUI); 

            yield return null;
        }
    }

    /// <summary>
    /// 마우스가 UI에 위치했는지를 체크
    /// </summary>
    private bool IsPointerOverUI()
    {
        PointerEventData eventData = new(EventSystem.current)
        {
            position = Pointer.current.position.ReadValue()
        };

        var results = new List<RaycastResult>();
        GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();
        raycaster.Raycast(eventData, results);

        return results.Count > 0;
    }
}


