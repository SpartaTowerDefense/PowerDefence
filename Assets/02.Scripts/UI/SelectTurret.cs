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

            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.TryGetComponent<Turret>(out Turret turret))
                {
                    if (turret == lastTurret) return;
                    lastTurret = turret;
                    grid.SetActive(true);
                    grid.transform.position = turret.gameObject.transform.position;
                    UIManager.Instance.curTurret = turret;

                    BindBtn(uiButtonHandler.SetCannonUpBtn(), turret.gameObject.GetComponent<CannonController>().ChangeCannon );
                    BindBtn(uiButtonHandler.SetBodyUpBtn(), turret.LevelUp);

                    return;
                }
            }
            if (grid.activeInHierarchy)
                grid.SetActive(false);
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

    void DeleteLastTurret()
    {
        lastTurret = null;
    }
}
