using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private int _attackRate;

    private Animator _animator;
    private float _lastAttackTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

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
        _animator.SetTrigger(AnimatorEnemyController.Triggers.Attack);
        Target.TakeDamage(_damage);
    }
}