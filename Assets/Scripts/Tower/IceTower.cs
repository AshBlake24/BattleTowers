using UnityEngine;

public class IceTower : Tower
{
    [SerializeField] private ParticleSystem _iceRingEffect;
    [SerializeField] private float _freezingTime;

    private void Update()
    {
        if (LastShootTime >= FiringRate)
        {
            Shot();

            LastShootTime = 0;
        }

        LastShootTime += Time.deltaTime;
    }

    private void Shot()
    {
        Instantiate(_iceRingEffect, FirePoint.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(FirePoint.position, FireRange, EnemiesLayerMask);

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.gameObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.Freeze(_freezingTime);
                }
            }
        }
    }
}