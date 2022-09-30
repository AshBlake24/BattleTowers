using UnityEngine;
using UnityEngine.UI;

public abstract class AttackTower : Tower
{
    protected const string CheckTargetsMethod = "CheckTargets";
    protected const float UpdateTargetsPerFrame = 2;

    [SerializeField] protected Arrow Arrow;
    [SerializeField] protected Transform FirePoint;
    [SerializeField] protected LayerMask EnemiesLayerMask;

    protected Enemy Target;

    protected abstract void Fire();

    protected void CheckTargets()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, FireRange, EnemiesLayerMask);

        if (colliders.Length > 0)
        {
            Enemy closestEnemy = null;
            float closestEnemyDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Enemy enemy) && enemy.IsAlive)
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
                Target = closestEnemy;
        }
        else
        {
            Target = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}
