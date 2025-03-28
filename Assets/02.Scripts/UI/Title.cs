using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject obj;
    private Camera _camera;
    private bool onStart = false;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void LateUpdate()
    {
        CameraFollow();
    }

    public void OnStart()
    {
        UIManager uiManager = UIManager.Instance;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DORotate(new Vector3(0, 0, -15), 0.5f))
           .Append(transform.DORotate(new Vector3(0, 0, -90), 1f))
           .OnComplete(() =>
           {
               uiManager.OnActive(false, gameObject.transform.parent.gameObject);
               Destroy(obj);
           });
        StartCoroutine(ChangeSet());
    }
    IEnumerator ChangeSet()
    {
        yield return new WaitForSeconds(0.5f);
        obj.GetComponent<SpriteRenderer>().DOFade(0, 0.99f);
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
