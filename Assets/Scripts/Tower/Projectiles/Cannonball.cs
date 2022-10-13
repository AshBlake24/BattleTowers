using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private int _damage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosionEffect;
    [SerializeField] private Vector3 _effectPositionOffset;

    [Header("Pool Settings")]
    [SerializeField] private int _effectPoolInitialCapacity;

    private static ObjectsPool<ParticleSystem> _effectPool;

    private LayerMask _enemiesLayerMask;

    private void Awake()
    {
        if (_effectPool != null)
            return;

        _effectPool = new ObjectsPool<ParticleSystem>(_explosionEffect.gameObject, _effectPoolInitialCapacity);
    }

    public void Init(LayerMask layerMask)
    {
        _enemiesLayerMask = layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        Helpers.ActivateEffectFromPool(_effectPool, transform.position + _effectPositionOffset, transform.rotation);

        Explode();

        gameObject.SetActive(false);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius, _enemiesLayerMask);

        if (colliders.Length > 0)
        {
            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                    enemy.TakeDamage(_damage);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _explosionRadius);
    }
}