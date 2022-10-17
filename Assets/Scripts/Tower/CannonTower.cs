using UnityEngine;

public class CannonTower : AttackTower
{
    private readonly float Gravity = Physics.gravity.y;

    [Header("Cannon Tower Settings")]
    [SerializeField] private Transform _cannon;
    [SerializeField] private float _angleInDegrees;
    [SerializeField] private float _rotationSpeed = 250;
    [SerializeField] private Cannonball _cannonball;

    private Vector3 _directionToTarget;
    private Vector3 _directionInAxisXZ;
    private Vector3 _firePointRotationOffset;

    public static ObjectPool<Cannonball> CannonballsPool { get; private set; }

    private void OnEnable() 
    {
        if (CannonballsPool == null)
            CannonballsPool = new ObjectPool<Cannonball>(_cannonball.gameObject);

        _firePointRotationOffset = FirePoint.transform.eulerAngles;

        FirePoint.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f) + _firePointRotationOffset;
        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        if (Target == null || Target.IsAlive == false)
            return;

        FindDirectionsToTarget();

        RotateToTarget();

        if (LastShootTime >= FiringRate)
        {
            Shot();

            LastShootTime = 0;
        }

        LastShootTime += Time.deltaTime;
    }

    protected override void Shot()
    {
        float speed = GetInitialSpeed();

        Cannonball cannonball = CannonballsPool.GetInstance();
        cannonball.gameObject.SetActive(true);
        cannonball.transform.SetLocalPositionAndRotation(FirePoint.position, Quaternion.identity);
        cannonball.Init(EnemiesLayerMask);

        cannonball.GetComponent<Rigidbody>().velocity = FirePoint.forward * speed;
    }

    private void FindDirectionsToTarget()
    {
        _directionToTarget = Target.transform.position - FirePoint.position;
        _directionInAxisXZ = new Vector3(_directionToTarget.x, 0f, _directionToTarget.z);
    }

    private void RotateToTarget()
    {
        Quaternion lookRotation = Quaternion.LookRotation(_directionInAxisXZ, Vector3.up);

        _cannon.rotation = Quaternion.RotateTowards(_cannon.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
    }

    private float GetInitialSpeed()
    {
        float x = _directionInAxisXZ.magnitude;
        float y = _directionToTarget.y;

        float angleInRadians = _angleInDegrees * Mathf.Deg2Rad;

        float speedSquared = (Gravity * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));

        return Mathf.Sqrt(Mathf.Abs(speedSquared));
    }
}