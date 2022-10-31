using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cannonball : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private ParticleSystem _explosionEffect;

    private LayerMask _enemiesLayerMask;

    public static ObjectPool<ParticleSystem> EffectPool { get; private set; }
    public int Damage => _damage;

    private void OnEnable()
    {
        if (EffectPool == null)
            EffectPool = new ObjectPool<ParticleSystem>(_explosionEffect.gameObject);
    }

    public void Init(LayerMask layerMask)
    {
        _enemiesLayerMask = layerMask;
    }

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem effect = EffectPool.GetInstance();
        effect.gameObject.SetActive(true);
        effect.transform.SetPositionAndRotation(transform.position, transform.rotation);

        Explode();

        CannonTower.CannonballsPool.AddInstance(this);
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