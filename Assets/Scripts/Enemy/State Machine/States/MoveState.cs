using UnityEngine;

public class MoveState : State
{
    private const float RotationTime = 0.1f;

    [SerializeField] private float _speed;

    private void Update()
    {
        Vector3 direction = Target.transform.position - transform.position;

        if (direction.magnitude >= 0.01f)
        {
            Move();
            Rotate(direction);
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
    }

    public void Rotate(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

        float zAxisRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, lookRotation.eulerAngles.y, ref _currentVelocity, RotationTime);

        transform.rotation = Quaternion.Euler(0, zAxisRotation, 0);
    }
}