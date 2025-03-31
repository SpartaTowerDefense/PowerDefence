using System.Collections;
using UnityEngine;

public class TestObj : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private Transform[] trans;
    private Coroutine moveRoutine;
    public float speed = 1;

    private void Start()
    {
        if (UIManager.Instance.Title.obj == null)
            UIManager.Instance.Title.obj = gameObject;

        trans = new Transform[obj.transform.childCount];
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            trans[i] = obj.transform.GetChild(i);
        }

        moveRoutine = StartCoroutine(ToTarget());
    }

    IEnumerator ToTarget()
    {
        for (int i = 0; i < trans.Length; i++)
        {
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
