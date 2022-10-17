using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private int _speed;
    [SerializeField] private ParticleSystem _impactEffect;

    private Enemy _target;

    public static ObjectPool<ParticleSystem> EffectPool { get; private set; }

    private void OnEnable()
    {
        if (EffectPool == null)
            EffectPool = new ObjectPool<ParticleSystem>(_impactEffect.gameObject);
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (_target.transform.position - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Euler(lookRotation.eulerAngles);

        transform.Translate(direction * _speed * Time.deltaTime, Space.World);
    }

    public void Init(Enemy target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            ParticleSystem effect = EffectPool.GetInstance();
            effect.gameObject.SetActive(true);
            effect.transform.SetPositionAndRotation(transform.position, transform.rotation); 

            enemy.TakeDamage(_damage);
        }

        ArcheryTower.ArrowsPool.AddInstance(this);
    }
}