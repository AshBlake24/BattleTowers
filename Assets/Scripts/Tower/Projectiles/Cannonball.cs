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

    private void OnEnable()
    {
        if (_effectPool == null)
            _effectPool = new ObjectsPool<ParticleSystem>(_explosionEffect.gameObject);
    }

    public void Init(LayerMask layerMask)
    {
        _enemiesLayerMask = layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        var effect = _effectPool.GetInstance();

        effect.gameObject.SetActive(true);
        effect.transform.SetPositionAndRotation(transform.position + _effectPositionOffset, transform.rotation);

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