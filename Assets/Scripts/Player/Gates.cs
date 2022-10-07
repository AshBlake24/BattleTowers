using System;
using UnityEngine;

public class Gates : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private Animator _gatesAnimator;

    private int _currentHealth;

    public event Action<int> HealthChanged;
    public event Action Destroyed;

    public bool IsDestroyed { get; private set; }

    private void Start()
    {
        _currentHealth = _health;
        IsDestroyed = false;

        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _gatesAnimator.SetTrigger(GatesAnimationController.Triggers.Hit);

        if (_currentHealth <= 0)
        {
            IsDestroyed = true;
            _gatesAnimator.SetBool(GatesAnimationController.Triggers.Destroy, true);
            Destroyed?.Invoke();
        }

        HealthChanged?.Invoke(_currentHealth);
    }
}