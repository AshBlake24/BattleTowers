using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class WaypointFollower : MonoBehaviour
{
    private Waypoint[] _waypoints;

    public Waypoint CurrentWaypoint { get; private set; }
    public int CurrentWaypointIndex { get; private set; }
    protected Transform Path { get; private set; }

    private void Start()
    {
        _waypoints = Path.GetComponentsInChildren<Waypoint>();

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

    public void Init(Transform path)
    {
        Path = path;
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