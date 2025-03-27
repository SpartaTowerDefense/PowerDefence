using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [SerializeField] private Tilemap roadtileMap;
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private TurretFactory turretFactory;

    public bool TryPlaceTank(Vector3 worldPos, BodyData bodyData)
    {
        Vector3Int cellPos = roadtileMap.WorldToCell(worldPos); // 주어진 월드좌표가 TileMap의 셀좌표로 변환
        Vector3 capturedPos = roadtileMap.CellToWorld(cellPos); // 다시 받은 셀좌표를 통해 다시 월드좌표로 변환

        if (!CanPlaceTank(cellPos)) //설치 못하는 타일이라면
        {
            return false;
        }

        GameObject newTank = turretFactory.CreateObject(null, -1); //일단 회전은 0으로 고정

        if(newTank == null)
        {
            return false;
        }
        newTank.transform.position = capturedPos;

        return true;
    }

    private bool CanPlaceTank(Vector3Int cellPos) //타일맵에서 셀좌표의 위치에 어떤 타일이 깔려있는지를 체크
    {
        TileBase road = roadtileMap.GetTile(cellPos); // 로드타일맵에 어떤 타일이 있는지 데이터를 가져오고 
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
