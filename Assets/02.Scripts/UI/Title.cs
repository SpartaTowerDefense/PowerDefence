using DG.Tweening;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class Title : MonoBehaviour
{
    UIButtonHandler uIButtonHandler;

    public GameObject obj;
    private Camera _camera;
    private bool onStart = false;

    private ButtonHover[] childs;

    private void Start()
    {
        uIButtonHandler = UIManager.Instance.UIButtonHandler;
        _camera = Camera.main;
        uIButtonHandler.BindButton(uIButtonHandler.SetStartBtn(), GameStart);

        childs = GetComponentsInChildren<ButtonHover>();
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    void GameStart()
    {
        foreach (var child in childs)
        {
            child.EndEffect();
            StartCoroutine(DisableNextFrame(child));
        }
        ButtonEffect effect = gameObject.GetComponentInChildren<ButtonEffect>();
        if (effect != null)
        {
            effect.enabled = false;
        }
        UIManager uiManager = UIManager.Instance;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 0, -15), 0.5f))
           .Append(transform.DORotate(new Vector3(0, 0, -90), 1f))
           .OnComplete(() =>
           {
               uiManager.ActiveCnavasChild(false, gameObject.transform.parent.gameObject);
               TestObj test = obj.GetComponent<TestObj>();
               if (test != null)
                   test.StopToTarget();
           });
        StartCoroutine(ChangeSet());

        //스포너 시작 연결 - 더 좋은 방법이 있을지도...
        ((EnemyFactory)FactoryManager.Instance.path[nameof(EnemyFactory)]).Gamestart();
    }

    IEnumerator DisableNextFrame(MonoBehaviour target)
    {
        yield return null;
        target.enabled = false;
    }

    IEnumerator ChangeSet()
    {
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<SpriteRenderer>().DOFade(0, 0.9f);
        _camera.transform.DOMove(new Vector3(0, 0, -10), 1f);
        _camera.DOOrthoSize(5, 1);
        onStart = true;
    }
    void CameraFollow()
    {
        if (obj == null || onStart) return;
        _camera.orthographicSize = 2;
        _camera.transform.position = new Vector3(obj.transform.position.x + 2, obj.transform.position.y, _camera.transform.position.z);

    }

}
