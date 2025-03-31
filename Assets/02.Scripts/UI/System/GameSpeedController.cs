using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    UIButtonHandler uiButtonHandler;

    [SerializeField] private int maxSpeed = 3;
    private int speedCount = 1;
    private bool onPause = false;

    private void Start()
    {
        uiButtonHandler = UIManager.Instance.UIButtonHandler;
        uiButtonHandler.BindButton(uiButtonHandler.SetPauseBtn(), Pause);
        uiButtonHandler.BindButton(uiButtonHandler.SetSpeedBtn(), ChangeSpeed);
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
