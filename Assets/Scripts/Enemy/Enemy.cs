using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyStateMachine), typeof(Collider))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _reward;
    [SerializeField] private int _score;
    [SerializeField] private ParticleSystem _freezeEffect;
    [SerializeField] private EnemyType _type;

    private Collider _collider;
    private Coroutine _freezeEffectCoroutine;
    private ParticleSystem _currentFreezeEffect;
    private EnemyStateMachine _stateMachine;
    private int _currentHealth;

    public event Action<Enemy, bool> Died;
    public event Action<int, int> HealthChanged;

    public static ObjectPool<ParticleSystem> FreezeEffectPool { get; private set; }

    public bool IsAlive { get; private set; }
    public bool IsFreezing { get; private set; }
    public bool KilledByPlayer { get; private set; }
    public int Reward => _reward;
    public int Score => _score;
    public EnemyType Type => _type;

    private void OnEnable()
    {
        if (_collider == null)
            _collider = GetComponent<Collider>();

        if (_stateMachine == null)
            _stateMachine = GetComponent<EnemyStateMachine>();

        if (FreezeEffectPool == null)
            FreezeEffectPool = new ObjectPool<ParticleSystem>(_freezeEffect.gameObject);

        _currentHealth = _health;

        IsAlive = true;
        IsFreezing = false;
        _collider.enabled = true;
    }

    public void Init(Player player)
    {
        HealthChanged.Invoke(_currentHealth, _health);

        _stateMachine.Init(player, this);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _health);

        HealthChanged?.Invoke(_currentHealth, _health);

        if (_currentHealth <= 0)
        {
            Die(true);
            Died?.Invoke(this, KilledByPlayer);
        }
    }

    public void Freeze(float freezingTime)
    {
        if (_freezeEffectCoroutine == null)
            _freezeEffectCoroutine = StartCoroutine(FreezingCooldown(freezingTime));
        else
            RestartFreeze(freezingTime);
    }

    public void Die()
    {
        Die(false);
        Died?.Invoke(this, KilledByPlayer);
    }

    private void Die(bool killedByPlayer)
    {
        _collider.enabled = false;
        KilledByPlayer = killedByPlayer;
        IsAlive = false;
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

public enum EnemyType
{
    Knight,
    Swordsman
}