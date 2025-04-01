using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private List<Tilemap> roadTile; 
    [SerializeField] private List<Tilemap> groundTile;
    [SerializeField] private List<Tilemap> obstacleTile;

    private Tilemap GroundTile => groundTile[curStageIndex];
    private Tilemap RoadTile => roadTile[curStageIndex];
    private Tilemap ObstacleTile => obstacleTile[curStageIndex];

    private int curStageIndex = 0;

    [Header("Object")]
    [SerializeField] private TurretFactory turretFactory; //터렛을 생성하는 기본적인 팩토리
    // [SerializeField] private GameObject tankPrefab; 

    private Quaternion currentRot = Quaternion.identity; // 현재 배치할 터렛의 회전값

    private HashSet<Vector3Int> occupiedCell = new(); // 터렛이 같은 위치에 배치되지 않도록 만들기 위해 저장하기 위한 Collection

    public Vector3Int WorldToCell(Vector3 worldPos) //주어진 월드좌표가 TileMap의 셀좌표로 변환
    {
        return GroundTile.WorldToCell(worldPos);
    }

    public Vector3 SnapToCenter(Vector3Int cellPos) //다시 받은 셀좌표를 통해 다시 월드좌표 및 Tile의 포지션에 맞게 정렬
    {
        return GroundTile.CellToWorld(cellPos) + new Vector3(0.5f, 0.5f, 0);
    }

    /// <summary>
    /// 터렛 배치를 시도
    /// </summary>
    public bool TryPlaceTurret(Vector3 worldPos, TurretData bodyData)
    {
        Vector3Int cellPos = WorldToCell(worldPos); 
        Vector3 capturedPos = SnapToCenter(cellPos);
        
        if (!CanPlaceTurret(cellPos)) //설치 못하는 타일이라면
        {
            return false;
        }

        int enumType = (int)bodyData.Type; //터렛 종류(enum)을 통해서 정수로 변환하고 ObjectPoolManager를 통해서 해당 오브젝트의 데이터를 로드
        Debug.Log(enumType);
        GameObject newTank = ObjectPoolManager.Instance.GetObject<TurretFactory>(enumType);  

        if(newTank == null)
        {
            return false;
        }
        newTank.transform.position = capturedPos;  // 위치 및 회전 설정 후 배치
        newTank.transform.rotation = GetCurrentRotation();

        occupiedCell.Add(cellPos);  //Hashset에 해당 셀을 이미 사용된 위치로 등록

        return true;
    }

    /// <summary>
    /// 해당 셀 좌표에 터렛을 설치할 수 있는지 검사
    /// </summary>
    public bool CanPlaceTurret(Vector3Int cellPos) 
    {
        bool isGround = GroundTile.GetTile(cellPos) != null;
        bool isRoad = RoadTile.GetTile(cellPos) != null;
        bool isObstacle = ObstacleTile.GetTile(cellPos) != null;
        bool isOccupied = occupiedCell.Contains(cellPos);

        return isGround && !isRoad && !isOccupied &&!isObstacle;  // ground 타일이 존재하고, 도로도 아니고, 장애물도 아니고, 이미 사용된 셀도 아니어야 함
    }

    /// <summary>
    /// 현재 회전값을 90도씩 시계 방향으로 회전
    /// </summary>
    public void Rotate()
    {
        currentRot *= Quaternion.Euler(0, 0, 90f);
    }

    /// <summary>
    /// 현재 회전값을 반환
    /// </summary>
    public Quaternion GetCurrentRotation()
    {
        return currentRot;
    }

    public void SetStageIndex(int index)
    {
        curStageIndex = index;
        occupiedCell.Clear(); // 새 스테이지에서는 기존 설치 좌표 초기화
    }
}