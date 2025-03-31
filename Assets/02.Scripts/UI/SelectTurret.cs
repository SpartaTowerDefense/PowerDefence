using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cannonInfo;
    [SerializeField] TextMeshProUGUI cannonUpgradePirce;
    [SerializeField] TextMeshProUGUI bodyInfo;
    [SerializeField] TextMeshProUGUI bodyUpgradePirce;

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
    void ClickTurretInfo()
    {
        Turret turret = UIManager.Instance.curTurret;
        CannonController cannonController= turret.gameObject.GetComponent<CannonController>();
        if (turret != null)
        {
            cannonInfo.text = cannonController.level.ToString();
            //cannonUpgradePirce.text = cannonController.
            bodyInfo.text = turret.Level.ToString();
            bodyUpgradePirce.text = turret.TurretStat.Price.ToString();
        }
        else
        {
            cannonInfo.text = string.Empty;
            cannonUpgradePirce.text = string.Empty;
            bodyInfo.text = string.Empty;
            bodyUpgradePirce.text = string.Empty;
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
