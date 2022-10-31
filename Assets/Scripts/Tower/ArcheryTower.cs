using UnityEngine;

public class ArcheryTower : AttackTower
{
    [Header("Archery Tower Settings")]
    [SerializeField] private Arrow _arrow;

    public static ObjectPool<Arrow> ArrowsPool { get; private set; }

    private void OnEnable()
    {
        if (ArrowsPool == null)
            ArrowsPool = new ObjectPool<Arrow>(_arrow.gameObject);

        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        LastShootTime += Time.deltaTime;

        if (Target == null || Target.IsAlive == false)
            return;

        if (LastShootTime >= 1f / AttackPerSecond)
        {
            Shot();

            LastShootTime = 0;
        }
    }

    public override int GetDamage()
    {
        return _arrow.Damage;
    }

    protected override void Shot()
    {
        Arrow arrow = ArrowsPool.GetInstance();
        arrow.gameObject.SetActive(true);
        arrow.transform.SetPositionAndRotation(FirePoint.position, FirePoint.rotation);
        arrow.Init(Target);
    }
}