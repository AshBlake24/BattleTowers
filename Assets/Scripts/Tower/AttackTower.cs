using UnityEngine;

public abstract class AttackTower : Tower
{
    protected Enemy Target;

    protected override void CheckTargets()
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
}