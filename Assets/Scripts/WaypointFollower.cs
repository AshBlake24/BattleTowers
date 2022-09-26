using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private Waypoint[] _waypoints;

    public Waypoint CurrentWaypoint { get; private set; }
    public int CurrentWaypointIndex { get; private set; }

    private void Start()
    {
        _waypoints = _path.GetComponentsInChildren<Waypoint>();

        CurrentWaypointIndex = 0;

        if (_waypoints.Length > 0)
            CurrentWaypoint = _waypoints[CurrentWaypointIndex];
    }

    private void Update()
    {
        if (CurrentWaypoint == null)
            return;

        if (transform.position == CurrentWaypoint.transform.position)
            SetNextWaypoint();
    }

    public void SetNextWaypoint()
    {
        CurrentWaypointIndex++;

        if (CurrentWaypointIndex >= _waypoints.Length)
            CurrentWaypoint = null;
        else
            CurrentWaypoint = _waypoints[CurrentWaypointIndex];
    }
}