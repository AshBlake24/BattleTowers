using System;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(WaypointFollower))]
public class MoveState : State
{
    private const float RotationTime = 0.15f;

    [SerializeField] private float _speed;
    [SerializeField] private float _debuffSpeed;

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
        _animator.SetBool(AnimatorEnemyController.States.Run, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(AnimatorEnemyController.States.Run, false);
    }

    private void Update()
    {
        if (_follower.CurrentWaypoint == null)
            return;

        _target = _follower.CurrentWaypoint.transform;
        _direction = _target.position - transform.position;

        Move();

        if (_direction.magnitude >= 0.01f)
            Rotate();
    }

    public void Move()
    {
        if (Self.IsFreezing)
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _debuffSpeed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    }

    public void Rotate()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_direction, Vector3.up);

        float zAxisRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _currentVelocity, RotationTime);

        transform.rotation = Quaternion.Euler(0, zAxisRotation, 0);
    }
}