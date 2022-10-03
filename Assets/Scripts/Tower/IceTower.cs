using UnityEngine;

public class IceTower : Tower
{
    [Header("Ice Tower Settings")]
    [SerializeField] private ParticleSystem _iceRingEffect;
    [SerializeField] private float _freezingTime;

    private Collider[] _colliders;

    private void OnEnable()
    {
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
        Instantiate(_iceRingEffect, FirePoint.position, Quaternion.identity);

        foreach (var collider in _colliders)
        {
            if (collider.gameObject.TryGetComponent(out Enemy enemy))
            {
                enemy.Freeze(_freezingTime);
            }
        }
    }
}