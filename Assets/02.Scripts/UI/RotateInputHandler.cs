using UnityEngine;

public class RotateInputHandler : MonoBehaviour
{
    [SerializeField] private DragHandler dragHandler;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            UIManager.Instance.Placement.Rotate();
        }
    }
}
