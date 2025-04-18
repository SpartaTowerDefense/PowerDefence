using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button speedBtn;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button loadBtn;
    [SerializeField] private Button cannonUpBtn;
    [SerializeField] private Button bodyUpBtn;
    [SerializeField] private Button reStartBtn;

    public Button SetPauseBtn() => pauseBtn;
    public Button SetSpeedBtn() =>speedBtn;
    public Button SetStartBtn() => startBtn;
    public Button SetCannonUpBtn() => cannonUpBtn;
    public Button SetBodyUpBtn() => bodyUpBtn;
    public Button SetLoadBtn() => loadBtn;
    public Button SetReStartBtn() => reStartBtn;

    public void SetInteractable(Button btn, bool enable)
    {
        if(btn.interactable != enable)
            btn.interactable = enable;
    }
    public void BindButton(Button button, params UnityAction[] actions)
    {
        button.onClick.RemoveAllListeners();
        foreach (var action in actions)
        {
            if(action != null)
                button.onClick.AddListener(action);
        }
    }
}
