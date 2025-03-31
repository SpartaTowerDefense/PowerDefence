using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoHelper : Singleton<MonoHelper>
{
    public new Coroutine StartCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    public new void StopCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }
}
