using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    [SerializeField] private List<GameObject> maps;
    private GameObject curMap;
    private Transform[] targetTransform;
    private Coroutine moveRoutine;
    public float speed = 1;

    private void Start()
    {
        if (UIManager.Instance.Title.obj == null)
            UIManager.Instance.Title.obj = gameObject;

        for(int i = 0; i < maps.Count; i++)
        {
            if(i == GameManager.Instance.CurrentStage)  curMap = maps[i];
        }

        targetTransform = new Transform[curMap.transform.childCount];

        for (int i = 0; i < curMap.transform.childCount; i++)
        {
            targetTransform[i] = curMap.transform.GetChild(i);
        }
        transform.position = targetTransform[0].position;

        moveRoutine = StartCoroutine(ToTarget());
    }

    IEnumerator ToTarget()
    {
        for (int i = 0; i < targetTransform.Length; i++)
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = targetTransform[i].position;

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
    public void StopToTarget()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
            moveRoutine = null;
        }
        Destroy(gameObject);
    }

}
