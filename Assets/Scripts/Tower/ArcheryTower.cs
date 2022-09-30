using UnityEngine;

public class ArcheryTower : AttackTower
{
    private void OnEnable()
    {
        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        if (Target == null || Target.IsAlive == false)
            return;

        if (LastShootTime >= FiringRate)
        {
            Fire();

            LastShootTime = 0;
        }

        LastShootTime += Time.deltaTime;
    }

    protected override void Fire()
    {
        var projectile = Instantiate(Arrow, FirePoint.position, FirePoint.rotation, transform);
        projectile.Init(Target);
    }
}