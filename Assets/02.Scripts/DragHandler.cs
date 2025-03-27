using UnityEngine;

public class DragHandler : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private Canvas canvas;
    //[SerializeField] private PlaceManager placeManager;

    [Header("Tank Sprite")]
    [SerializeField] private Sprite bodySprite;
    [SerializeField] private Sprite turretSprite;

    private RectTransform rectTransform; 
    private Vector2 originalPosition; //최초 위치
    private bool isDrag = false;
    private bool isPlace = false;





}
