using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    private GameObject[] squareArray;
    private RectTransform[] rectTransformArray;
    private Image[] imageArray;

    private List<Tween> shakeTweens = new List<Tween>();

    private void Start()
    {
        CreateSquare(3);
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
    }

    public void ChangeSquare(RectTransform rectTransform)
    {
        for (int i = 0; i < squareArray.Length; i++)
        {
            rectTransformArray[i].sizeDelta = rectTransform.sizeDelta * rectTransform.localScale * Random.Range(1f, 1.2f);
            rectTransformArray[i].position = rectTransform.position;

            imageArray[i].color = ChnageColor(0.6f);
        }
    }

    //채도 높은 색만 뽑아 알파값 조절
    Color ChnageColor(float alpha)
    {
        float h = Random.Range(0f, 1f);
        float s = Random.Range(0.7f, 1f);
        float v = Random.Range(0.8f, 1f);
        Color color = Color.HSVToRGB(h, s, v);
        color.a = alpha;

        return color;
    }
    void KillTween(List<Tween> tween)
    {
        foreach (Tween t in tween)
        {
            t.Kill();
        }
        tween.Clear();
    }

    public void ShakeSquare()
    {
        KillTween(shakeTweens);
        Sequence shakeSequence = DOTween.Sequence();
        foreach (RectTransform trans in rectTransformArray)
        {
            shakeSequence.Append(trans.DOShakePosition(10, 10, 10, 90, false, false));
        }
        shakeSequence.SetLoops(-1, LoopType.Restart);
        shakeTweens.Add(shakeSequence);
    }

    public void StopShakeSquare()
    {
        KillTween(shakeTweens);
    }
}
