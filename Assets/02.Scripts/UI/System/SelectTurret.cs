using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTurret : MonoBehaviour
{
    [SerializeField] private SelectTurretUI selectTurretUI;

    private Turret lastTurret;

    private void Start()
    {
        selectTurretUI.OnRequestClearSelection = ClearSelection;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TurretClick();
        }
    }

    void TurretClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.TryGetComponent<Turret>(out Turret turret))
        {
            if (turret == lastTurret) return;

            lastTurret = turret;
            UIManager.Instance.curTurret = turret;

            selectTurretUI.DisplayTurret(
                turret,
                turret.GetComponent<CannonController>().ChangeCannon,
                turret.LevelUp
            );
        }
        else
        {
            ClearSelection();
            selectTurretUI.ClearUI();
        }
    }

    void ClearSelection()
    {
        lastTurret = null;
        UIManager.Instance.curTurret = null;
    }
}
