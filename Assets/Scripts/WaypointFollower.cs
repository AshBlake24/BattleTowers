using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;

    private Transform[] _waypoints;
    private int _currentWaypoint;

    private void Start()
    {
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = _path.GetChild(i);
        }
    }

    private void Update()
    {
        Transform target = _waypoints[_currentWaypoint];

        //Vector3 direction = (target.position - transform.position).normalized;

        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _currentWaypoint++;

            if (_currentWaypoint >= _waypoints.Length)
            {
                _currentWaypoint = 0;
            }
        }
    }
}
