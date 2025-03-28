using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [SerializeField] private Tilemap roadTile;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private TurretFactory turretFactory;

    public bool TryPlaceTank(Vector3 worldPos, TurretData bodyData)
    {
        Vector3Int cellPos = roadTile.WorldToCell(worldPos); // 주어진 월드좌표가 TileMap의 셀좌표로 변환
        Vector3 capturedPos = roadTile.CellToWorld(cellPos); // 다시 받은 셀좌표를 통해 다시 월드좌표로 변환

        if (!CanPlaceTank(cellPos)) //설치 못하는 타일이라면
        {
            return false;
        }

        int enumType = (int)bodyData.Type;
        Debug.Log(enumType);
        GameObject newTank = ObjectPoolManager.Instance.GetObject<TurretFactory>(enumType); 

        if(newTank == null)
        {
            return false;
        }
        newTank.transform.position = capturedPos;

        return true;
    }

    public  bool CanPlaceTank(Vector3Int cellPos) //타일맵에서 셀좌표의 위치에 어떤 타일이 깔려있는지를 체크
    {
        TileBase road = roadTile.GetTile(cellPos); // 로드타일맵에 어떤 타일이 있는지 데이터를 가져오고 
                                                      // 추가적으로 장애물 설치시에도 가능함 조건을 추가
        if(road != null)
        {
            return false; //타일이 있다면 도로이기 때문에 설치 불가로 false를 반환
        }
        else
        {
            return true;
        }
    }
}
