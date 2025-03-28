using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class DragHandler : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Canvas canvas; // 마우스의 좌표를 UI로 변환할 때 필요한 변수
    [SerializeField] private Placement placement; 

    //[Header("Tank Sprite")]
    //[SerializeField] private Sprite bodySprite;
    //[SerializeField] private Sprite turretSprite;

    private RectTransform rectTransform; // UI 오브젝트를 마우스의 좌표로 움직이게 만들기 위한 변수
    private Vector2 originalPosition; // UI상 초기 위치
    private bool isDrag = false;
    private bool isPlace = false;

    private PlayerInput inputActions;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        inputActions = new PlayerInput();
    }

    private void Start()
    {
        originalPosition = rectTransform.anchoredPosition; //UI 아이콘의 초기 위치를 기준 앵커포인트로부터의 상대 위치로 저장
    }

    private void Update()
    {
        if (isPlace) return; //아직 배치중이라면 빠져나가도록

        if(isDrag)
        {
            Vector2 mouseScreenPos = inputActions.PlayerActions.MousePosition.ReadValue<Vector2>();

            rectTransform.anchoredPosition = mouseScreenPos / canvas.scaleFactor;
        }
        
    }
    private void OnEnable() 
    {
        inputActions.Enable(); //액션 활성화

        inputActions.PlayerActions.Click.started += OnLeftClickStarted;  // Input로직 구독
        inputActions.PlayerActions.Click.started += OnLeftClickCanceled;
    }

    private void OnDisable()
    {
        inputActions.PlayerActions.Click.canceled -= OnLeftClickStarted; //구독 해제
        inputActions.PlayerActions.Click.canceled -= OnLeftClickCanceled;

        inputActions.Disable(); //액션 비활성화
    }

    private void OnLeftClickStarted(InputAction.CallbackContext context) //드래그시작시
    {
        if (isPlace) return;
        isDrag = true;
    }

    private void OnLeftClickCanceled(InputAction.CallbackContext context) //드래그 종료시
    {
        if (!isDrag || isPlace) return; //
            
        isDrag = false;

        BodyData selectedData = UIManager.Instance.Shop.curData;

        if(selectedData == null)
        {
            rectTransform.anchoredPosition = originalPosition;
        }

        Vector2 mouseScreenPos = inputActions.PlayerActions.MousePosition.ReadValue<Vector2>();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreenPos.x, mouseScreenPos.y, 0f)
        ); 
        worldPos.z = 0f; //z축은 고정

        bool isSuccess = placement.TryPlaceTank(worldPos, selectedData);
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

}
