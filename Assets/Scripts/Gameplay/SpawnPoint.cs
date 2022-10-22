using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Waypoint _startWaypoint;

    public Waypoint StartWaypoint => _startWaypoint;
}