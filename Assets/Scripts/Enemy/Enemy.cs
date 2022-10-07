using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _freezeEffectPrefab;

    private ParticleSystem _freezeEffect;
    private Coroutine _freezeEffectCoroutine;

    public event Action<Enemy> Died;

    public Gates Target { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsFreezing { get; private set; }

    private void OnEnable()
    {
        IsAlive = true;
        IsFreezing = false;

        _freezeEffect = Instantiate(_freezeEffectPrefab, transform.position, transform.rotation, transform);
        _freezeEffect.Stop();
    }

    public void Init(Gates target)
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
        if (_freezeEffectCoroutine == null)
            _freezeEffectCoroutine = StartCoroutine(FreezingCooldown(freezingTime));
        else
            RestartFreeze(freezingTime);
    }

    private void RestartFreeze(float freezingTime)
    {
        StopCoroutine(_freezeEffectCoroutine);

        _freezeEffectCoroutine = null;

        _freezeEffectCoroutine = StartCoroutine(FreezingCooldown(freezingTime));
    }

    private IEnumerator FreezingCooldown(float freezingTime)
    {
        IsFreezing = true;
        _freezeEffect.Play();

        yield return Helpers.GetTime(freezingTime);

        IsFreezing = false;
        _freezeEffect.Stop();
    }
}