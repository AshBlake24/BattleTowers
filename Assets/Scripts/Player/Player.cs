using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _startMoney;

    private int _money;

    public event Action<int> MoneyChanged;

    public int Money => _money;

    private void Start()
    {
        _money = _startMoney;
        MoneyChanged?.Invoke(_money);
    }

    public void Buy(int price)
    {
        if ((_money - price) < 0)
            return;

        _money -= price;

        MoneyChanged?.Invoke(_money);
    }
}