using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class WaypointFollower : MonoBehaviour
{
    private Waypoint _previousWaypoint;
    private Waypoint _nextWaypoint;

    public Waypoint CurrentWaypoint { get; private set; }

    private void Update()
    {
        if (CurrentWaypoint == null)
            return;

        if (transform.position == CurrentWaypoint.transform.position)
            SetNextWaypoint();
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