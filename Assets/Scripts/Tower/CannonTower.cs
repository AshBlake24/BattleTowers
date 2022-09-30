using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTower : AttackTower
{
    private readonly float g = Physics.gravity.y;

    [SerializeField] private Transform _cannon;
    [SerializeField] private Cannonball _cannonball;
    [SerializeField] private float _angleInDegrees;

    private void OnEnable() 
    {
        FirePoint.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);
        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        if (Target == null || Target.IsAlive == false)
            return;

        if (LastShootTime >= FiringRate)
        {
            Shot();

            LastShootTime = 0;
        }

        LastShootTime += Time.deltaTime;
    }

    protected override void Shot()
    {
        Vector3 direction = Target.transform.position - FirePoint.position;
        Vector3 directionXZ = new Vector3(direction.x, 0f, direction.z);

        _cannon.rotation = Quaternion.LookRotation(directionXZ, Vector3.up);

        float x = directionXZ.magnitude;
        float y = direction.y;

        float angleInRadians = _angleInDegrees * Mathf.Deg2Rad;

        float speedSquared = (g * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float speed = Mathf.Sqrt(Mathf.Abs(speedSquared));

        Cannonball cannonball = Instantiate(_cannonball, FirePoint.position, Quaternion.identity);

        cannonball.GetComponent<Rigidbody>().velocity = FirePoint.forward * speed;
    }
}
