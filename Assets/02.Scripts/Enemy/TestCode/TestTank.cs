using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTank : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform spawnPoint;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
