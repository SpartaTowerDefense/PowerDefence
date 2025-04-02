using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPanel : MonoBehaviour
{
    private void OnEnable()
    {
        UIManager.Instance.UIButtonHandler.BindButton(UIManager.Instance.UIButtonHandler.SetReStartBtn(), RestartBtn);
        GameEnd();
    }

    public void GameEnd()
    {
        UIManager.Instance.MainCanvas.ActiveCnavasChild(true, UIManager.Instance.EndPanel.gameObject);
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, -6), 0.1f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, 4), 0.08f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, -2), 0.07f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(Vector3.zero, 0.05f).SetEase(Ease.OutSine));
    }

    void RestartBtn()
    {
        Time.timeScale = 1f;
        FactoryManager.Instance.ClearPath();
        ObjectPoolManager.Instance.ClearObjectPool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
