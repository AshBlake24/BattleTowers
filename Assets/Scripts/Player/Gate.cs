using System;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public event Action<int, int> HealthChanged;
    public event Action Destroyed;

    public bool IsDestroyed { get; private set; }

    private void Start()
    {
        _currentHealth = _health;
        IsDestroyed = false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            IsDestroyed = true;
            Destroyed?.Invoke();
        }

        HealthChanged?.Invoke(_currentHealth, _health);
    }
}