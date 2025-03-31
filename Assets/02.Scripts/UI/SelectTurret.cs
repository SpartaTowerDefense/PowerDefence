using UnityEngine;
using UnityEngine.EventSystems;

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

                    uiButtonHandler.BindButton(uiButtonHandler.SetBodyUpBtn(),
                        () => uiButtonHandler.SetInteractable(uiButtonHandler.SetBodyUpBtn(),false), 
                        turret.LevelUp, 
                        ActiveFalse, 
                        DeleteLastTurret);
                    uiButtonHandler.SetInteractable(uiButtonHandler.SetBodyUpBtn(), true);
                    
                    return;
                }
            }
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

    void ActiveFalse()
    {
        grid.SetActive(false);
    }

    void DeleteLastTurret()
    {
        lastTurret = null;
    }
}
