using UnityEngine;

public class ArcheryTower : AttackTower
{
    [Header("Archery Tower Settings")]
    [SerializeField] private Arrow _arrow;

    private void OnEnable()
    {
        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        LastShootTime += Time.deltaTime;

        if (Target == null || Target.IsAlive == false)
            return;

        if (LastShootTime >= FiringRate)
        {
            Shot();

            LastShootTime = 0;
        }
    }

    protected override void Shot()
    {
        Arrow arrow = Instantiate(_arrow, FirePoint.position, FirePoint.rotation, transform);
        arrow.Init(Target);
    }
}