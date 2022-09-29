using UnityEngine;

public class Arrow : Projectile
{
    private Enemy _target;

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

        transform.Translate(direction * Speed * Time.deltaTime, Space.World);
    }

    public void Init(Enemy target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(Damage);
        }

        Destroy(gameObject);
    }
}