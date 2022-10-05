using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _nextWaypoints;

    public Waypoint GetNextWaypoint()
    {
        switch (_nextWaypoints.Count)
        {
            case 0: 
                return null;
            case 1: 
                return _nextWaypoints[0];
            default:
                return GetRandomWaypoint();
        }
    }

    private Waypoint GetRandomWaypoint()
    {
        return _nextWaypoints[Random.Range(0, _nextWaypoints.Count)];
    }
}