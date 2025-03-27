using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Turret turret;

    private void Awake()
    {
        turret = GetComponentInParent<Turret>();
    }
    private void Start()
    {
        
    }
}
