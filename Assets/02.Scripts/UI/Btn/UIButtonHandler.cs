using UnityEngine;
using UnityEngine.UI;

public class UIButtonHandler : MonoBehaviour
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button speedBtn;
    [SerializeField] private Button startBtn;

    [SerializeField] private Button cannonUpBtn;
    [SerializeField] private Button bodyUpBtn;

    [SerializeField] private int maxSpeed = 3;
    private int speedCount = 1;
    private bool onPause = false;

    private void Start()
    {
        pauseBtn.onClick.AddListener(Pause);
        speedBtn.onClick.AddListener(ChangeSpeed);
        startBtn.onClick.AddListener(UIManager.Instance.Title.GameStart);
    }

    void Pause()
    {
        onPause = !onPause;
        Time.timeScale = onPause ? 0f : speedCount;
        Debug.Log(Time.timeScale);
    }

    void ChangeSpeed()
    {
        speedCount = (speedCount < maxSpeed) ? speedCount + 1 : 1;
        if (!onPause)
            Time.timeScale = speedCount;
        Debug.Log(Time.timeScale);
    }
}
