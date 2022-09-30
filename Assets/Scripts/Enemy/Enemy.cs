using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;

    public event Action<Enemy> Died;

    public Gate Target { get; private set; }
    public bool IsAlive { get; private set; }

    private void OnEnable()
    {
        IsAlive = true;
    }

    public void Init(Gate target)
    {
        Target = target;
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            IsAlive = false;
            Died?.Invoke(this);
        }
    }
}