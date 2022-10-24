using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _startMoney;

    private int _money;
    private int _score;
    private int _currentHealth;

    public event Action<int> MoneyChanged;
    public event Action<int> HealthChanged;
    public event Action Died;

    public int Money => _money;
    public int Score => _score;
    public bool IsAlive { get; private set; }

    private void Start()
    {
        IsAlive = true;

        _money = _startMoney;
        _currentHealth = _health;

        MoneyChanged?.Invoke(_money);
        HealthChanged?.Invoke(_currentHealth);
    }

    public void TakeDamage()
    {
        _currentHealth--;

        HealthChanged?.Invoke(_currentHealth);

        if (_currentHealth <= 0)
        {
            IsAlive = false;
            Died?.Invoke();
        }
    }

    public void TakeMoney(int price)
    {
        if ((_money - price) < 0)
            return;

        _money -= price;

        MoneyChanged?.Invoke(_money);
    }

    public void AddMoney(int money)
    {
        _money += money;

        MoneyChanged?.Invoke(_money);
    }

    public void AddScore(int score)
    {
        _score += score;
    }
}