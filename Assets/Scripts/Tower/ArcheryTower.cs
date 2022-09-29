using UnityEngine;

public class ArcheryTower : Tower
{
    private const string CheckTargetsMethod = "CheckTargets";
    private const float UpdateTargetsPerFrame = 2;

    [SerializeField] private Arrow _projectile;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireRange;

    private Enemy _target;
    private int _enemiesLayerMask;
    
    private void OnEnable()
    {
        _enemiesLayerMask = 1 << EnemiesLayerMask;
        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        if (_target == null)
            return;

        if (LastShootTime >= FiringRate)
        {
            Fire();

            LastShootTime = 0;
        }

        LastShootTime += Time.deltaTime;
    }

    private void CheckTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _fireRange, _enemiesLayerMask);

        if (colliders.Length > 0)
        {
            Enemy closestEnemy = null;
            float closestEnemyDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);

                    if (enemyDistance < closestEnemyDistance)
                    {
                        closestEnemyDistance = enemyDistance;
                        closestEnemy = enemy;
                    }
                }
            }

            if (closestEnemy != null)
                _target = closestEnemy;
        }
        else
        {
            _target = null;
        }
    }

    private void Fire()
    {
        var projectile = Instantiate(_projectile, _firePoint.position, _firePoint.rotation, transform);
        projectile.Init(_target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _fireRange);
    }
}