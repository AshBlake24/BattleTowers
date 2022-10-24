using System;

public class EndPointTransition : Transition
{
    private WaypointFollower _follower;

    private void Awake()
    {
        _follower = GetComponent<WaypointFollower>();
    }

    private void OnEnable()
    {
        _follower.WaypointReached += OnWaypointReached;
    }

    private void OnDisable()
    {
        _follower.WaypointReached -= OnWaypointReached;
    }

    private void OnWaypointReached(Waypoint waypoint)
    {
        if (waypoint.IsEndPoint)
        {
            ReadyToTransition = true;
            Player.TakeDamage();
        }
    }
}