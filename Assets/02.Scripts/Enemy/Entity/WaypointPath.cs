using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform[] waypoints;

    public Transform GetWaypoint(int index)
    {
        if (index < 0 || index >= waypoints.Length) return null;
        return waypoints[index];
    }

    public int WaypointCount => waypoints.Length;
}
