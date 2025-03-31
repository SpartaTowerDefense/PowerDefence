using DG.Tweening;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    [SerializeField] private int createNum = 3;

    private GameObject[] squareArray;
    private RectTransform[] rectTransformArray;
    private Image[] imageArray;

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

        for (int i = 0; i < squareArray.Length; i++)
        {
            squareArray[i] = new GameObject("Square");
            squareArray[i].AddComponent<RectTransform>();
            squareArray[i].AddComponent<Image>();
            squareArray[i].GetComponent<RectTransform>().SetParent(transform, false);
            Image image = squareArray[i].GetComponent<Image>();
            image.raycastTarget = false;

            rectTransformArray[i] = squareArray[i].GetComponent<RectTransform>();
            imageArray[i] = squareArray[i].GetComponent<Image>();
        }
    }

    public void OnActiveSquare(bool square)
    {
        foreach (GameObject obj in this.squareArray)
        {
            obj?.SetActive(square);
        }
        if (!square) KillTween(shakeTweens);
    }

    public void ChangeSquare(RectTransform rectTransform)
    {
        for (int i = 0; i < squareArray.Length; i++)
        {
            rectTransformArray[i].sizeDelta = rectTransform.sizeDelta * rectTransform.localScale * Random.Range(1f, 1.2f);
            rectTransformArray[i].position = rectTransform.position;

            imageArray[i].color = ChangeColor(0.6f);
        }
    }

    //채도 높은 색만 뽑아 알파값 조절
    Color ChangeColor(float alpha)
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
        KillTween(shakeTweens);
        Sequence shakeSequence = DOTween.Sequence();
        foreach (RectTransform trans in rectTransformArray)
        {
            shakeSequence.Join(trans.DOShakePosition(10, 10, 10, 90, false, false));
        }
        shakeSequence.SetLoops(-1, LoopType.Restart);
        shakeTweens.Add(shakeSequence);
    }
    public void ClickEffectSquare(UnityAction action = null)
    {
        KillTween(clickTweens);

        for (int i = 0; i < squareArray.Length; i++)
        {
            Sequence clickSequence = DOTween.Sequence();
            RectTransform originRect = rectTransformArray[i];
            clickSequence
                .Append(rectTransformArray[i].DOScale(originRect.localScale * 0.5f, 0.2f))
                .Append(rectTransformArray[i].DOScale(Vector3.one, 0.2f))
                .SetEase(Ease.OutQuad)
                .OnComplete(() => action?.Invoke());
            clickTweens.Add(clickSequence);
        }
    } 

    void KillTween(List<Tween> tween)
    {
        foreach (Tween t in tween)
        {
            t.Kill();
        }
        tween.Clear();
    }
}
