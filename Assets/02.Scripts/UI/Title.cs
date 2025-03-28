using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public GameObject obj;
    private Camera _camera;

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
           .OnComplete(() => uiManager.OnActive(false, gameObject.transform.parent.gameObject));
        Invoke("BaseCam", 0.5f);
    }
    void BaseCam()
    {
        Destroy(obj);
        _camera.transform.DOMove(new Vector3(0, 0, -10), 1f);
        _camera.DOOrthoSize(5, 1);
    }
    void CameraFollow()
    {
        if (obj == null) return;

        _camera.transform.position = new Vector3(obj.transform.position.x + 2, obj.transform.position.y,_camera.transform.position.z);
        _camera.orthographicSize = 2;
    }
    
}
