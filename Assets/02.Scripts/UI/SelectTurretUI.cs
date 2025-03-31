
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectTurretUI : MonoBehaviour
{
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TextMeshProUGUI cannonLv;
    [SerializeField] private TextMeshProUGUI cannonUpgradePrice;
    [SerializeField] private TextMeshProUGUI bodyLv;
    [SerializeField] private TextMeshProUGUI bodyUpgradePrice;
    [SerializeField] private GameObject grid;

    UIButtonHandler uiButtonHandler;

    public UnityAction OnRequestClearSelection;

    private void Start()
    {
        uiButtonHandler = UIManager.Instance.UIButtonHandler;

        textPanel.SetActive(false);
        grid = Instantiate(grid);
        grid.SetActive(false);

        ClearUI();
    }

    public void DisplayTurret(Turret turret, UnityAction onCannonUpgrade, UnityAction onBodyUpgrade)
    {
        if (turret == null) return;

        grid.SetActive(true);
        grid.transform.position = turret.transform.position;

        BindButton(uiButtonHandler.SetCannonUpBtn(), onCannonUpgrade);
        BindButton(uiButtonHandler.SetBodyUpBtn(), onBodyUpgrade);

        var cannon = turret.GetComponent<CannonController>();
        textPanel.SetActive(true);
        cannonLv.text = cannon.level.ToString();
        cannonUpgradePrice.text = cannon.Price.ToString();
        bodyLv.text = turret.Level.ToString();
        bodyUpgradePrice.text = turret.TurretStat.Price.ToString();
    }

    public void ClearUI()
    {
        grid.SetActive(false);
        textPanel.SetActive(false);
        cannonLv.text = string.Empty;
        cannonUpgradePrice.text = string.Empty;
        bodyLv.text = string.Empty;
        bodyUpgradePrice.text = string.Empty;

        uiButtonHandler.SetCannonUpBtn().interactable = false;
        uiButtonHandler.SetBodyUpBtn().interactable = false;
    }

    private void BindButton(Button button, UnityAction action)
    {
        uiButtonHandler.BindButton(button, action, () =>
        {
            OnRequestClearSelection?.Invoke(); 
        });
        uiButtonHandler.SetInteractable(button, true);
    }
}

