using UnityEngine;

public class ArcheryTower : AttackTower
{
    [Header("Archery Tower Settings")]
    [SerializeField] private Arrow _arrow;

    [Header("Pool Settings")]
    [SerializeField] private int _arrowPoolInitialCapacity;

    private static ObjectsPool<Arrow> _arrowPool;

    private void OnEnable()
    {
        if (_arrowPool == null)
            _arrowPool = new ObjectsPool<Arrow>(_arrow.gameObject);

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
        Arrow arrow = _arrowPool.GetInstance();

        arrow.gameObject.SetActive(true);
        arrow.transform.SetPositionAndRotation(FirePoint.transform.position, FirePoint.transform.rotation);
        arrow.Init(Target);
    }
}