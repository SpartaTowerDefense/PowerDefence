
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SelectTurretUI : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TextMeshProUGUI cannonLv;
    [SerializeField] private TextMeshProUGUI cannonUpgradePrice;
    [SerializeField] private TextMeshProUGUI bodyLv;
    [SerializeField] private TextMeshProUGUI bodyUpgradePrice;
    [SerializeField] private GameObject grid;
    [SerializeField] private Image _cameraMask;
    [SerializeField] private Camera _camera;

    private UIButtonHandler uiButtonHandler;
    private readonly Color visible = new Color(1, 1, 1, 1);
    private readonly Color invisible = new Color(1, 1, 1, 0);

    public UnityAction OnRequestClearSelection;

    private void Start()
    {
        uiButtonHandler = UIManager.Instance.UIButtonHandler;

        textPanel.SetActive(false);
        grid = Instantiate(grid);
        grid.SetActive(false);
        _camera.enabled = false;
        _cameraMask.color = invisible;

        ClearUI();
    }

    public void DisplayTurret(Turret turret, UnityAction onCannonUpgrade, UnityAction onBodyUpgrade)
    {
        if (turret == null) return;

        grid.SetActive(true);
        grid.transform.position = turret.transform.position;

        _camera.enabled = true;
        _camera.transform.position = new Vector3(turret.transform.position.x, turret.transform.position.y, -10f);
        _cameraMask.color = visible;

        BindButton(uiButtonHandler.SetCannonUpBtn(), onCannonUpgrade, turret);
        BindButton(uiButtonHandler.SetBodyUpBtn(), onBodyUpgrade, turret);
    }

    public void UpdateUI(Turret turret)
    {
        var cannon = turret.GetComponent<CannonController>();
        textPanel.SetActive(true);
        cannonLv.text = cannon.level.ToString();
        cannonUpgradePrice.text = cannon.Price.ToString();
        bodyLv.text = turret.Level.ToString();
        bodyUpgradePrice.text = turret.TurretStat.Price.ToString();
    }

    public void ClearUI()
    {
        _cameraMask.color = invisible;
        _camera.enabled = false;

        grid.SetActive(false);
        textPanel.SetActive(false);
        
        cannonLv.text = string.Empty;
        cannonUpgradePrice.text = string.Empty;
        bodyLv.text = string.Empty;
        bodyUpgradePrice.text = string.Empty;

        uiButtonHandler.SetInteractable(uiButtonHandler.SetCannonUpBtn(), false);
        uiButtonHandler.SetInteractable(uiButtonHandler.SetBodyUpBtn(), false);
    }

    private void BindButton(Button button, UnityAction action, Turret turret)
    {
        uiButtonHandler.BindButton(button, action,() =>
        {
            UpdateUI(turret);
            OnRequestClearSelection?.Invoke();
        });
        uiButtonHandler.SetInteractable(button, true);
    }
}

