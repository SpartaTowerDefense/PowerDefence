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
        UIManager.Instance.ActiveCnavasChild(true, UIManager.Instance.EndPanel.gameObject);
        Sequence seq = DOTween.Sequence();

        seq.Append(transform.DORotate(new Vector3(0, 0, 10), 0.1f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, -6), 0.1f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, 4), 0.08f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(new Vector3(0, 0, -2), 0.07f).SetEase(Ease.OutSine));
        seq.Append(transform.DORotate(Vector3.zero, 0.05f).SetEase(Ease.OutSine));
    }

    void RestartBtn()
    {
        DOTween.KillAll();

        Destroy(GameManager.Instance.gameObject);
        Destroy(UIManager.Instance.gameObject);
        Destroy(AudioManager.Instance.gameObject);
        Destroy(FactoryManager.Instance.gameObject);
        Destroy(DataManager.Instance.gameObject);
        Destroy(ObjectPoolManager.Instance.gameObject);
        Destroy(ResourceManager.Instance.gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    
}
