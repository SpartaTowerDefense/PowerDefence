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

    private Vector3 originPosition;
    private Quaternion originRotation;

    private void Awake()
    {
        originPosition = transform.position;
        originRotation = transform.rotation;
    }
    private void Start()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;

        uIButtonHandler = UIManager.Instance.UIButtonHandler;
        _camera = Camera.main;
        uIButtonHandler.BindButton(uIButtonHandler.SetStartBtn(), GameStart);
        uIButtonHandler.BindButton(uIButtonHandler.SetLoadBtn(), Load,GameStart);
        childs = GetComponentsInChildren<ButtonHover>();
    }
    private void OnEnable()
    {
        transform.position = originPosition;
        transform.rotation = originRotation;
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
               uiManager.MainCanvas.ActiveCnavasChild(false, gameObject.transform.parent.gameObject);
               TestObj test = obj.GetComponent<TestObj>();
               if (test != null)
                   test.StopToTarget();
           });
        StartCoroutine(ChangeSet());

        //스포너 시작 연결 - 더 좋은 방법이 있을지도...
        ((EnemyFactory)FactoryManager.Instance.path[nameof(EnemyFactory)]).Gamestart();
        GameManager.Instance.SaveGame();

        
    }

    void Load()
    {
        GameManager.Instance.LoadGame();
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
        Camera.main.transform.DOMove(new Vector3(0, 0, -10), 1f);
        Camera.main.DOOrthoSize(5, 1);
        onStart = true;
    }
    void CameraFollow()
    {
        if (obj == null || onStart) return;
        Camera.main.orthographicSize = 2;
        Camera.main.transform.position = new Vector3(obj.transform.position.x + 2, obj.transform.position.y, Camera.main.transform.position.z);
    }

}
