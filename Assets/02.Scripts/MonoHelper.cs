using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHelper : Singleton<MonoHelper>
{

    public static Coroutine StartCoroutineHelper(IEnumerator coroutine)
    {
        return Instance.StartCoroutine(coroutine);
    }

    public static void StopCoroutineHelper(Coroutine coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }
}
