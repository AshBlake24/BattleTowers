using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private int _score;
    [SerializeField] private ParticleSystem _freezeEffect;

    private Coroutine _freezeEffectCoroutine;
    private ParticleSystem _currentFreezeEffect;
    private int _currentHealth;

    public event Action<Enemy> Died;
    public event Action<int, int> HealthChanged;

    public static ObjectPool<ParticleSystem> FreezeEffectPool { get; private set; }
    public Gates Target { get; private set; }
    public bool IsAlive { get; private set; }
    public bool IsFreezing { get; private set; }
    public int Reward => _reward;
    public int Score => _score;

    private void OnEnable()
    {
        if (FreezeEffectPool == null)
            FreezeEffectPool = new ObjectPool<ParticleSystem>(_freezeEffect.gameObject);


        IsAlive = true;
        IsFreezing = false;

        _currentHealth = _health;
    }

    public void Init(Gates target)
    {
        Target = target;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
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
        if (_currentFreezeEffect == null)
        {
            _currentFreezeEffect = FreezeEffectPool.GetInstance();
            _currentFreezeEffect.gameObject.SetActive(true);
            _currentFreezeEffect.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _currentFreezeEffect.transform.SetParent(transform);
        }

        _currentFreezeEffect.Play();
        IsFreezing = true;

        yield return Helpers.GetTime(freezingTime);

        IsFreezing = false;
        _currentFreezeEffect.Stop();
        _currentFreezeEffect = null;
    }
}