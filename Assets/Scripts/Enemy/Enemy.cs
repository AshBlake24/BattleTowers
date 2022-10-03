using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _freezeEffect;

    private bool _isFreezing;
    private Coroutine _freezingCooldown;

    public event Action<Enemy> Died;

    public Gate Target { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsFreezing => _isFreezing;

    private void OnEnable()
    {
        IsAlive = true;
        _isFreezing = false;
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

    public void Freeze(float freezingTime)
    {
        _isFreezing = true;

        if (_freezingCooldown != null)
        {
            StopCoroutine(FreezingCooldown(freezingTime));
            _freezingCooldown = StartCoroutine(FreezingCooldown(freezingTime));
        }
        else
        {
            _freezingCooldown = StartCoroutine(FreezingCooldown(freezingTime));
        }        
    }

    private IEnumerator FreezingCooldown(float freezingTime)
    {
        var freezeEffect = Instantiate(_freezeEffect, transform.position, transform.rotation);

        yield return Helpers.GetTime(freezingTime);

        _isFreezing = false;

        Destroy(freezeEffect);
    }
}