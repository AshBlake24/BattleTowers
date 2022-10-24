using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class WaypointFollower : MonoBehaviour
{
    private Waypoint _previousWaypoint;
    private Waypoint _nextWaypoint;

    public event Action<Waypoint> WaypointReached;

    public Waypoint CurrentWaypoint { get; private set; }

    private void Update()
    {
        if (CurrentWaypoint == null)
            return;

        if (transform.position == CurrentWaypoint.transform.position)
        {
            WaypointReached?.Invoke(CurrentWaypoint);
            SetNextWaypoint();
        }
    }

    public void Init(Waypoint startWaypoint)
    {
        CurrentWaypoint = startWaypoint;
    }

    public void SetNextWaypoint()
    {
        do _nextWaypoint = CurrentWaypoint.GetNextWaypoint();
        while (_previousWaypoint == _nextWaypoint);

        _previousWaypoint = CurrentWaypoint;
        CurrentWaypoint = _nextWaypoint;
    }
}