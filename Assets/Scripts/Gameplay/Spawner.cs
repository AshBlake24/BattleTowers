using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Wave _wave;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private float _delayBeforeWave;

    [Header("Target Settings")]
    [SerializeField] private Player _player;
    [SerializeField] private Gates _target;

    [Header("Pool Settings")]
    [SerializeField] private int _enemiesPoolInitialCapacity;

    private static Dictionary<EnemyType, ObjectsPool<Enemy>> _enemyPools;

    private int _currentWaveNumber;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private int _maxEnemiesInCurrentWave;
    private Coroutine _spawnEnemies;

    public event Action<int, int> EnemyKilled;
    public event Action<float> WaveStarted;
    public event Action WaveCleared;

    private void OnEnable()
    {
        if (_enemyPools == null)
        {
            _enemyPools = new Dictionary<EnemyType, ObjectsPool<Enemy>>();
            InitializeEnemyPools();
        }

        _currentWaveNumber = 1;

        _wave.ResetToDefault();

        SetWave();
    }

    private void Update()
    {
        if (_target.IsDestroyed)
        {
            if (_spawnEnemies != null)
            {
                StopCoroutine(_spawnEnemies);
                _spawnEnemies = null;
            }
        }

        if (_killedEnemies >= _wave.EnemiesCount)
        {
            Debug.Log("Killed enemies");
            _killedEnemies = 0;
            WaveCleared?.Invoke();
            SetNextWave();
        }
    }

    private void SetWave()
    {
        _wave.SetDifficulty(_currentWaveNumber);
        _killedEnemies = 0;

        StartCoroutine(LaunchWave());
    }

    private void SetNextWave()
    {
        _currentWaveNumber++;

        SetWave();
    }

    private void ResetWave()
    {
        _spawnedEnemies = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = _enemyPools[GetRandomEnemy()].GetInstance();

        enemy.gameObject.SetActive(true);
        enemy.transform.position = _spawnPoint.position;

        enemy.Init(_target);

        if (enemy.TryGetComponent(out WaypointFollower follower))
            follower.Init(_startWaypoint);

        var enemyHealthBar = enemy.GetComponentInChildren<EnemyHealthBar>();

        if (enemyHealthBar != null)
            enemyHealthBar.Init(enemy);

        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _enemyPools[enemy.Type].AddInstance(enemy);

        _killedEnemies++;

        _player.AddMoney(enemy.Reward);
        _player.AddScore(enemy.Score);

        EnemyKilled?.Invoke(_killedEnemies, _wave.EnemiesCount);

        enemy.Died -= OnEnemyDied;
    }

    private void InitializeEnemyPools()
    {
        Enemy[] enemies = _wave.GetEnemies();

        foreach (Enemy enemy in enemies)
        {
            if (_enemyPools.ContainsKey(enemy.Type))
                continue;

            _enemyPools.Add(enemy.Type, new ObjectsPool<Enemy>(enemy.gameObject));
        }
    }

    private EnemyType GetRandomEnemy()
    {
        return (EnemyType)UnityEngine.Random.Range(0, _enemyPools.Count);
    }

    private IEnumerator LaunchWave()
    {
        Debug.Log("Launch Wave");

        WaveStarted?.Invoke(_delayBeforeWave);

        yield return Helpers.GetTime(_delayBeforeWave);

        EnemyKilled?.Invoke(_killedEnemies, _wave.EnemiesCount);
        _spawnEnemies = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_spawnedEnemies < _wave.EnemiesCount)
        {
            Debug.Log("Spawn Enemies");

            InstantiateEnemy();

            _spawnedEnemies++;

            yield return Helpers.GetTime(_wave.SpawnRate);
        }

        Debug.Log("Spawn Done");

        ResetWave();
    }

    [Serializable]
    private class Wave
    {
        [Header("Wave Settings")]
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private int _startEnemiesCount;
        [SerializeField] private int _enemiesMultiplier;
        [SerializeField] private float _spawnRate;

        public float SpawnRate => _spawnRate;
        public int EnemiesCount { get; private set; }

        public void ResetToDefault()
        {
            EnemiesCount = _startEnemiesCount - _enemiesMultiplier;
        }

        public void SetDifficulty(int waveNumber)
        {
            EnemiesCount += waveNumber * _enemiesMultiplier;
        }

        public Enemy[] GetEnemies()
        {
            Enemy[] enemies = new Enemy[_enemies.Length];

            _enemies.CopyTo(enemies, 0);

            return enemies;
        }
    }
}