using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [SerializeField] private Tilemap roadtileMap;
    [SerializeField] private GameObject tankPrefab;

    //public bool TryPlaceTank(Vector3 worldPos, Sprite bodySprite, Sprite cannonSprite)
    //{
    //    Vector3Int cellPos = roadtileMap.WorldToCell(worldPos); // 주어진 월드좌표가 TileMap의 셀좌표로 변환
    //    Vector3 capturedPos = roadtileMap.CellToWorld(cellPos); // 다시 받은 셀좌표를 통해 다시 월드좌표로 변환

    //    if(!CanPlaceTank(cellPos)) //설치를 하지 못한다면
    //    {
    //        return false;
    //    }

    //    //탱크를 생성로직과 capturedPos를 이어주고 생성하면 끝.
    //    // 누군가 로직 생성시 삭제
    //    GameObject newTank = Instantiate(tankPrefab, capturedPos, Quaternion.identity); //일단 회전은 0으로 고정
        
    //    //Sprite를 교체하는 로직을 호출
    //    // 누군가 로직 생성시 삭제
    //}

    private bool CanPlaceTank(Vector3Int cellPos) //타일맵에서 셀좌표의 위치에 어떤 타일이 깔려있는지를 체크
    {
        TileBase tile = roadtileMap.GetTile(cellPos); //로드타일맵에 어떤 타일이 있는지 데이터를 가져오고
        if(tile != null)
        {
            return false; //타일이 있다면 도로이기 때문에 설치 불가로 false를 반환
        }
        else
        {
            return true;
        }
    }
}
