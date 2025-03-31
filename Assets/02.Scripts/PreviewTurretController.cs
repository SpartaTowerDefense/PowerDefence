using UnityEngine;

public class PreviewTurretController : MonoBehaviour
{
    public SpriteRenderer bodyRenderer;
    public SpriteRenderer squareRenderer;



    /// <summary>
    /// 프리뷰 본체 이미지 설정
    /// </summary>
    public void SetBodySprite(Sprite sprite)
    {
        if (bodyRenderer != null)
            bodyRenderer.sprite = sprite;
    }
    /// <summary>
    /// 설치 가능 여부에 따라 색상 변경
    /// </summary>
    public void SetPlacementColor(bool canPlace)
    {
        if (squareRenderer == null) return;

        squareRenderer.color = canPlace
            ? new Color(0f, 1f, 0f, 0.5f)    // 초록
            : new Color(1f, 0f, 0f, 0.5f);   // 빨강
    }
}
