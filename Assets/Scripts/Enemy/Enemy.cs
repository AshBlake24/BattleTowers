using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private ParticleSystem _freezeEffectPrefab;

    private bool _isFreezing;
    private ParticleSystem _freezeEffect;
    private Coroutine _freezeEffectCoroutine;

    public event Action<Enemy> Died;

    public Gate Target { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsFreezing => _isFreezing;

    private void OnEnable()
    {
        IsAlive = true;
        _isFreezing = false;

        _freezeEffect = Instantiate(_freezeEffectPrefab, transform.position, transform.rotation);
        _freezeEffect.Stop();
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
        _isFreezing = true;
        _freezeEffect.Play();

        yield return Helpers.GetTime(freezingTime);

        _isFreezing = false;
        _freezeEffect.Stop();
    }
}