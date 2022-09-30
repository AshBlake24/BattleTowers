using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballistics : MonoBehaviour
{
    private readonly float g = Physics.gravity.y;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Transform _target;
    [SerializeField] private float _angleInDegrees;

    private void Update()
    {
        _firePoint.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);

        if (Input.GetMouseButtonDown(0))
        {
            Shot();
        }
    }

    private void Shot()
    {
        Vector3 direction = _target.position - transform.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        transform.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);

        float x = directionXZ.magnitude;
        float y = direction.y;

        float angleInRadians = _angleInDegrees * Mathf.Deg2Rad;

        float v2 = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        GameObject bullet = Instantiate(_bullet, _firePoint.position, Quaternion.identity);

        bullet.GetComponent<Rigidbody>().velocity = _firePoint.forward * v;
    }

    public void Init(Transform firePoint, Transform target)
    {
        _firePoint = firePoint;
        _target = target;
    }
}
