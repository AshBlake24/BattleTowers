using UnityEngine;

[RequireComponent(typeof(WaypointFollower))]
public class MoveState : State
{
    private const float RotationTime = 0.1f;

    [SerializeField] private float _speed;

    private WaypointFollower _follower;
    private Waypoint _targetWaypoint;
    private float _currentVelocity;

    private void Start()
    {
        _follower = GetComponent<WaypointFollower>();
    }

    private void Update()
    {
        if (_follower.CurrentWaypoint == null)
            return;

        _targetWaypoint = _follower.CurrentWaypoint;

        Vector3 direction = _targetWaypoint.transform.position - transform.position;

        Move();

        if (direction.magnitude >= 0.01f)
            Rotate(direction);
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint.transform.position, _speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

        float zAxisRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _currentVelocity, RotationTime);

        transform.rotation = Quaternion.Euler(0, zAxisRotation, 0);
    }
}