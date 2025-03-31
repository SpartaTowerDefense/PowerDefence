using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Placement : MonoBehaviour
{
    [Header("Map")]
    [SerializeField] private Tilemap roadTile;
    [SerializeField] private Tilemap groundTile;
    [SerializeField] private Tilemap obstacleTile;

    [Header("Object")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private TurretFactory turretFactory;

    private Quaternion currentRot = Quaternion.identity;

    private HashSet<Vector3Int> occupiedCell = new(); // 터렛이 같은 위치에 배치되지 않도록 만들기 위해 저장한 Collection

    public bool TryPlaceTank(Vector3 worldPos, TurretData bodyData)
    {
        Vector3Int cellPos = groundTile.WorldToCell(worldPos); // 주어진 월드좌표가 TileMap의 셀좌표로 변환
        Vector3 capturedPos = groundTile.CellToWorld(cellPos); // 다시 받은 셀좌표를 통해 다시 월드좌표로 변환
        capturedPos += new Vector3(0.5f, 0.5f, 0);
        
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
        newTank.transform.rotation = GetCurrentRotation();
        occupiedCell.Add(cellPos); 

        return true;
    }

    public  bool CanPlaceTank(Vector3Int cellPos) //타일맵에서 셀좌표의 위치에 어떤 타일이 깔려있는지를 체크
    {
       
        bool isGround = groundTile.GetTile(cellPos) != null;
        bool isRoad = roadTile.GetTile(cellPos) != null;
        bool isObstacle = obstacleTile.GetTile(cellPos) != null;
        bool isOccupied = occupiedCell.Contains(cellPos);

        return isGround && !isRoad && !isOccupied &&!isObstacle; //Ground이면서 도로가 아닌 곳에서만 설치가 가능하도록
    }
    public void Rotate()
    {
        currentRot *= Quaternion.Euler(0, 0, 90f);
    }
    public Quaternion GetCurrentRotation()
    {
        return currentRot;
    }
}