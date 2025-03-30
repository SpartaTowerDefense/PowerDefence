using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RectTransform textRectTransform;
    Vector3 textPosition;
    Color textColor;

    TextMeshProUGUI text;
    ButtonEffect buttonEffect;
    RectTransform rectTransform;
    PointerEventData pointerEventData;

    private void Start()
    {
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        buttonEffect = UIManager.Instance.ButtonEffect;
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        pointerEventData = eventData;
        UIRayFindButton(pointerEventData, true);
        if (gameObject.TryGetComponent<Button>(out Button button))
        {
            ChangeButtonInText();
            buttonEffect.OnActiveSquare(true);
            buttonEffect.ChangeSquare(rectTransform);
            buttonEffect.ShakeSquare();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIRayFindButton(pointerEventData, false);
        if (gameObject.TryGetComponent<Button>(out Button button))
        {
            BackButtonInText();
            buttonEffect.OnActiveSquare(false);
            buttonEffect.StopShakeSquare();
        }
    }

    void ChangeButtonInText()
    {
        if (text != null)
        {
            textColor = text.color;
            textRectTransform = text.rectTransform.parent as RectTransform;
            textPosition = text.rectTransform.anchoredPosition;

            text.color = Color.white;

            Vector3 curPos = text.transform.position;
            text.rectTransform.SetParent(UIManager.Instance.transform, false);
            text.rectTransform.anchoredPosition = UIManager.Instance.transform.InverseTransformPoint(curPos);
        }
    }

    void BackButtonInText()
    {
        if (text != null)
        {
            text.color = textColor;
            text.rectTransform.anchoredPosition = textPosition;
            text.rectTransform.SetParent(textRectTransform, false);
        }
    }

    //자주 쓰는 ui 컴포먼트만 검사 raycastTarget의 위치가 다르기 때문에 전체 통합은 안된다고 판단
    void UIRayFindButton(PointerEventData eventData, bool enable)
    {
        var raycastObject = eventData.pointerCurrentRaycast.gameObject;

        if (raycastObject == null) return;

        if (raycastObject.TryGetComponent<Button>(out Button btn))
        {
            btn.gameObject.GetComponent<Image>().raycastTarget = true;
        }
        else if (raycastObject.TryGetComponent<Image>(out Image image))
        {
            image.raycastTarget = enable;
        }
        else if (raycastObject.TryGetComponent<Text>(out Text text))
        {
            text.raycastTarget = enable;
        }
        else return;
    }
}
