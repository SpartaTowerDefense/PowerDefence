using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private Transform[] trans;
    public float speed = 1;

    private void Start()
    {

        if(UIManager.Instance.Title.obj == null)
            UIManager.Instance.Title.obj = gameObject;

        trans = new Transform[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            trans[i] = obj.transform.GetChild(i).gameObject.transform;
        }

        StartCoroutine(Totarget());
    }

    IEnumerator Totarget()
    {
        for (int i = 0; i < trans.Length; i++)
        {
            //yield return transform.DOMove(trans[i].position, 1f).SetSpeedBased(true).SetEase(Ease.Linear).WaitForCompletion();

            Vector3 startPosition = transform.position;
            Vector3 targetPosition = trans[i].position;

            float moveSpeed = speed;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float duration = distance / moveSpeed;

            float timeElapsed = 0f;
            while (timeElapsed < duration)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
        }
    }
}
