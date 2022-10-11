using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private float _delayBeforeWave;

    [Header("Target Settings")]
    [SerializeField] private Player _player;
    [SerializeField] private Gates _target;

    [Header("Pool Settings")]
    [SerializeField] private int _enemiesPoolInitialCapacity;

    private static Dictionary<EnemyType, ObjectsPool<Enemy>> _enemyPools;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private int _maxEnemiesInCurrentWave;
    private Coroutine _spawnEnemies;

    public event Action<int, int> EnemyKilled;
    public event Action<float> WaveStarted;
    public event Action WaveCleared;

    private void Awake()
    {
        if (_enemyPools != null)
            return;

        _enemyPools = new Dictionary<EnemyType, ObjectsPool<Enemy>>();

        InitializeEnemyPools();
    }

    private void Start()
    {
        if (_waves != null & _waves.Length > 0)
            SetWave(_currentWaveIndex);
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

        if (_killedEnemies >= _maxEnemiesInCurrentWave)
        {
            _killedEnemies = 0;
            WaveCleared?.Invoke();
        }
    }

    public void SetNextWave()
    {
        if (_waves.Length > _currentWaveIndex + 1)
            SetWave(++_currentWaveIndex);
    }

    private void SetWave(int index)
    {
        if (_currentWave != null)
            ResetWave();

        _currentWave = _waves[index];
        _maxEnemiesInCurrentWave = _currentWave.EnemiesCount;
        _killedEnemies = 0;

        EnemyKilled?.Invoke(_killedEnemies, _maxEnemiesInCurrentWave);

        StartCoroutine(LaunchWave());
    }

    private void ResetWave()
    {
        _currentWave = null;
        _spawnedEnemies = 0;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = _enemyPools[GetRandomEnemy()].GetInstanceFromPool();

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
        _killedEnemies++;

        _player.AddMoney(enemy.Reward);
        _player.AddScore(enemy.Score);

        EnemyKilled?.Invoke(_killedEnemies, _maxEnemiesInCurrentWave);

        enemy.Died -= OnEnemyDied;
    }

    private void InitializeEnemyPools()
    {
        List<Enemy> enemies = GetAllEnemies();

        foreach (Enemy enemy in enemies)
        {
            if (_enemyPools.ContainsKey(enemy.Type))
                continue;

            _enemyPools.Add(enemy.Type, new ObjectsPool<Enemy>(enemy.gameObject, _enemiesPoolInitialCapacity));
        }
    }

    private EnemyType GetRandomEnemy()
    {
        return (EnemyType)UnityEngine.Random.Range(0, _enemyPools.Count);
    }

    private List<Enemy> GetAllEnemies()
    {
        var enemies = new List<Enemy>();

        foreach (Wave wave in _waves)
        {
            foreach (Enemy enemy in wave.Enemies)
            {
                if (enemies.Contains(enemy))
                    continue;

                enemies.Add(enemy);
            }
        }

        return enemies;
    }

    private IEnumerator LaunchWave()
    {
        WaveStarted?.Invoke(_delayBeforeWave);

        yield return Helpers.GetTime(_delayBeforeWave);

        _spawnEnemies = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_spawnedEnemies < _currentWave.EnemiesCount)
        {
            InstantiateEnemy();

            _spawnedEnemies++;

            yield return Helpers.GetTime(_currentWave.SpawnRate);
        }

        ResetWave();
    }

    [Serializable]
    private class Wave
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private int _enemiesCount;
        [SerializeField] private float _spawnRate;

        public Enemy[] Enemies => _enemies;
        public int EnemiesCount => _enemiesCount;
        public float SpawnRate => _spawnRate;
    }
}