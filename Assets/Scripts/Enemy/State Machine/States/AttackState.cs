using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private int _attackRate;

    private float _lastAttackTime;

    private void Update()
    {
        if (_lastAttackTime >= _attackRate)
        {
            Attack();

            _lastAttackTime = 0;
        }

        _lastAttackTime += Time.deltaTime;
    }

    private void Attack()
    {
        Target.TakeDamage(_damage);
    }
}