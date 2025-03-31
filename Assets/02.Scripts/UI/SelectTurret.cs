using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] private GameObject grid;
    private UIButtonHandler uiButtonHandler;
    private Turret lastTurret;

    private void Start()
    {
        uiButtonHandler = UIManager.Instance.UIButtonHandler;
        grid = Instantiate(grid);
        grid.SetActive(false);
    }

    void Update()
    {
        ClickTurret();
    }

    void ClickTurret()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Turret>(out Turret turret))
                {
                    if (turret == lastTurret) return;
                    lastTurret = turret;
                    grid.SetActive(true);
                    grid.transform.position = turret.gameObject.transform.position;
                    UIManager.Instance.curTurret = turret;

                    BindBtn(uiButtonHandler.SetCannonUpBtn());
                    BindBtn(uiButtonHandler.SetBodyUpBtn(), turret.LevelUp);

                    return;
                }
            }
            ActiveFalse();
            if (uiButtonHandler.SetCannonUpBtn())
                uiButtonHandler.SetCannonUpBtn().interactable = false;
            if(uiButtonHandler.SetBodyUpBtn())
                uiButtonHandler.SetBodyUpBtn().interactable = false;
            if (lastTurret != null)
                lastTurret = null;
            if (grid.activeInHierarchy)
                grid.SetActive(false);
            if (UIManager.Instance.curTurret != null)
                UIManager.Instance.curTurret = null;
        }
    }
    void BindBtn(Button button,UnityAction action = null)
    {
        uiButtonHandler.BindButton(button,
                        action,
                        DeleteLastTurret);
        uiButtonHandler.SetInteractable(button, true);
    }
    void ActiveFalse()
    {
        if(grid.activeInHierarchy)
            grid.SetActive(false);
    }

    void DeleteLastTurret()
    {
        lastTurret = null;
    }
}
