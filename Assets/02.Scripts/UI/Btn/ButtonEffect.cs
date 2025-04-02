using DG.Tweening;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    [Header("Square 설정")]
    [SerializeField] private int createNum = 3;
    [SerializeField] private Color squareColor = new Color(1, 1, 1, 0.5f);

    [Header("Shake 설정")]
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private float shakeStrength = 10f;
    [SerializeField] private int shakeVibrato = 10;
    [SerializeField] private float shakeRandomness = 90f;

    private GameObject[] squareArray;
    private RectTransform[] rectTransformArray;
    private Image[] imageArray;
    private Vector3[] originScales;

    private List<Tween> shakeTweens = new List<Tween>();
    private List<Tween> clickTweens = new List<Tween>();

    private void Start()
    {
        CreateSquare(createNum);
        OnActiveSquare(false);
    }

    void CreateSquare(int num)
    {
        squareArray = new GameObject[num];
        rectTransformArray = new RectTransform[num];
        imageArray = new Image[num];
        originScales = new Vector3[num];

        for (int i = 0; i < num; i++)
        {
            GameObject square = new GameObject($"Square_{i}", typeof(RectTransform), typeof(Image));
            square.transform.SetParent(transform, false);

            RectTransform rt = square.GetComponent<RectTransform>();
            Image img = square.GetComponent<Image>();

            img.color = squareColor;
            img.raycastTarget = false;

            squareArray[i] = square;
            rectTransformArray[i] = rt;
            imageArray[i] = img;
            originScales[i] = rt.localScale;
        }
    }

    public void OnActiveSquare(bool isActive)
    {
        foreach (GameObject obj in squareArray)
        {
            obj?.SetActive(isActive);
        }

        if (!isActive)
            AllKillTween();
    }

    public void ChangeTransformSquare(RectTransform target)
    {
        for (int i = 0; i < rectTransformArray.Length; i++)
        {
            float scaleMultiplier = Random.Range(1f, 1.2f);
            rectTransformArray[i].sizeDelta = target.sizeDelta * scaleMultiplier;
            rectTransformArray[i].position = target.position;
        }
    }

    public void ChangeColorSquare()
    {
        for (int i = 0; i < imageArray.Length; i++)
        {
            imageArray[i].color = GetRandomColor(0.6f);
        }
    }

    Color GetRandomColor(float alpha)
    {
        float h = Random.Range(0f, 1f);
        float s = Random.Range(0.7f, 1f);
        float v = Random.Range(0.8f, 1f);
        Color color = Color.HSVToRGB(h, s, v);
        color.a = alpha;
        return color;
    }

    public void ShakeSquare()
    {
        CompleteEffect(shakeTweens);
        KillTween(shakeTweens);

        Sequence shakeSequence = DOTween.Sequence();

        foreach (RectTransform rt in rectTransformArray)
        {
            shakeSequence.Join(
                rt.DOShakePosition(
                    shakeDuration,
                    shakeStrength,
                    shakeVibrato,
                    shakeRandomness,
                    false,
                    false
                )
            );
        }

        shakeSequence.SetLoops(-1, LoopType.Restart);
        shakeTweens.Add(shakeSequence);
    }

    public void ClickEffectSquare(UnityAction onComplete = null)
    {
        CompleteEffect(clickTweens);
        KillTween(clickTweens);

        bool invoked = false;

        for (int i = 0; i < rectTransformArray.Length; i++)
        {
            Sequence clickSequence = DOTween.Sequence();

            clickSequence
                .Append(rectTransformArray[i].DOScale(originScales[i] * 0.5f, 0.2f))
                .Append(rectTransformArray[i].DOScale(originScales[i], 0.2f))
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    if (!invoked)
                    {
                        invoked = true;
                        onComplete?.Invoke();
                    }
                });

            clickTweens.Add(clickSequence);
        }
    }

    public void AllCompleteTween()
    {
        CompleteEffect(shakeTweens);
        CompleteEffect(clickTweens);
    }

    public void CompleteEffect(List<Tween> tweenList)
    {
        if (tweenList == null) return;

        foreach (var tween in tweenList)
        {
            tween.Play();
            tween.Complete();
        }
    }

    public void AllKillTween()
    {
        KillTween(shakeTweens);
        KillTween(clickTweens);
    }

    public void KillTween(List<Tween> tweenList)
    {
        foreach (Tween t in tweenList)
        {
            t?.Kill();
        }

        tweenList.Clear();
    }
}

