using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _startMoney;

    private int _money;
    private int _score;

    public event Action<int> MoneyChanged;

    public int Money => _money;
    public int Score => _score;

    private void Start()
    {
        _money = _startMoney;
        MoneyChanged?.Invoke(_money);
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