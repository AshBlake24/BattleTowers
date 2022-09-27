using UnityEngine;

[RequireComponent(typeof(WaypointFollower))]
[RequireComponent(typeof(Animator))]
public class MoveState : State
{
    private const string Moving = "Moving";
    private const float RotationTime = 0.1f;

    [SerializeField] private float _speed;

    private Animator _animator;
    private WaypointFollower _follower;
    private Transform _target;
    private Vector3 _direction;
    private float _currentVelocity;

    private void Awake()
    {
        _follower = GetComponent<WaypointFollower>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.SetBool(Moving, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(Moving, false);
    }

    private void Update()
    {
        if (_follower.CurrentWaypoint != null)
            _target = _follower.CurrentWaypoint.transform;
        else
            _target = Target.transform;

        _direction = _target.position - transform.position;

        Move();

        if (_direction.magnitude >= 0.01f)
            Rotate();
    }

    public void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void Rotate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_direction, Vector3.up);

        float zAxisRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _currentVelocity, RotationTime);

        transform.rotation = Quaternion.Euler(0, zAxisRotation, 0);
    }
}