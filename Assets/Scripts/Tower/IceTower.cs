using UnityEngine;

public class IceTower : Tower
{
    [Header("Ice Tower Settings")]
    [SerializeField] private ParticleSystem _iceRingEffect;
    [SerializeField] private float _freezingTime;

    [Header("Pool Settings")]
    [SerializeField] private int _effectPoolInitialCapacity;

    private static ObjectsPool<ParticleSystem> _effectPool;

    private Collider[] _colliders;

    private void OnEnable()
    {
        if (_effectPool == null)
            _effectPool = new ObjectsPool<ParticleSystem>(_iceRingEffect.gameObject);

        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        LastShootTime += Time.deltaTime;

        if (_colliders.Length <= 0)
            return;

        if (LastShootTime >= FiringRate)
        {
            Shot();

            LastShootTime = 0;
        }
    }

    protected override void CheckTargets()
    {
        _colliders = Physics.OverlapSphere(FirePoint.position, FireRange, EnemiesLayerMask);
    }

    protected override void Shot()
    {
        var effect = _effectPool.GetInstance();

        effect.gameObject.SetActive(true);
        effect.transform.SetPositionAndRotation(FirePoint.position, Quaternion.identity);

        foreach (var collider in _colliders)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                if (enemy.IsAlive)
                    enemy.Freeze(_freezingTime);
            }
        }
    }
}