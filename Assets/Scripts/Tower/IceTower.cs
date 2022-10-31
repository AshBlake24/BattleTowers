using UnityEngine;

public class IceTower : Tower
{
    [Header("Ice Tower Settings")]
    [SerializeField] private ParticleSystem _iceRingEffect;
    [SerializeField] private float _freezingTime;

    private Collider[] _colliders;

    public static ObjectPool<ParticleSystem> EffectPool { get; private set; }
    public float Duration => _freezingTime;

    private void OnEnable()
    {
        if (EffectPool == null)
            EffectPool = new ObjectPool<ParticleSystem>(_iceRingEffect.gameObject);

        InvokeRepeating(CheckTargetsMethod, 0, 1 / UpdateTargetsPerFrame);
    }

    private void Update()
    {
        LastShootTime += Time.deltaTime;

        if (_colliders.Length <= 0)
            return;

        if (LastShootTime >= 1f / AttackPerSecond)
        {
            Shot();

            LastShootTime = 0;
        }
    }

    public override int GetDamage()
    {
        return 0;
    }

    protected override void CheckTargets()
    {
        _colliders = Physics.OverlapSphere(FirePoint.position, FireRange, EnemiesLayerMask);
    }

    protected override void Shot()
    {
        ParticleSystem effect = EffectPool.GetInstance();
        effect.gameObject.SetActive(true);
        effect.transform.SetPositionAndRotation(FirePoint.position, Quaternion.identity);

        foreach (var collider in _colliders)
        {
            if (collider == null)
                continue;

            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Freeze(_freezingTime);
            }
        }
    }
}