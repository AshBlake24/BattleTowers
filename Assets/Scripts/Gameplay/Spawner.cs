using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private Wave _wave;

    [Header("Spawner Settings")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private float _delayBeforeWave;

    [Header("Target Settings")]
    [SerializeField] private Player _player;
    [SerializeField] private Gates _target;

    public event Action<int, int> EnemyKilled;
    public event Action<float> WaveStarted;
    public event Action WaveCleared;

    private int _currentWaveNumber;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private Coroutine _spawnEnemies;

    private void Start()
    {
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
        Enemy enemy = Instantiate(GetRandomEnemy(), _spawnPoint);

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

        EnemyKilled?.Invoke(_killedEnemies, _wave.EnemiesCount);

        enemy.Died -= OnEnemyDied;
    }

    private Enemy GetRandomEnemy()
    {
        int enemyIndex = UnityEngine.Random.Range(0, _wave.Enemies.Length);

        return _wave.Enemies[enemyIndex];
    }

    private IEnumerator LaunchWave()
    {
        WaveStarted?.Invoke(_delayBeforeWave);

        yield return Helpers.GetTime(_delayBeforeWave);

        _spawnEnemies = StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while (_spawnedEnemies < _wave.EnemiesCount)
        {
            InstantiateEnemy();

            _spawnedEnemies++;

            yield return Helpers.GetTime(_wave.SpawnRate);
        }

        ResetWave();
    }

    [Serializable]
    private class Wave
    {
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private int _startEnemiesCount;
        [SerializeField] private int _enemiesWaveMultiplier;
        [SerializeField] private float _spawnRate;

        public Enemy[] Enemies => _enemies;
        public float SpawnRate => _spawnRate;
        public int EnemiesCount { get; private set; }

        public void SetDifficulty(int waveNumber)
        {
            EnemiesCount += waveNumber * _enemiesWaveMultiplier;
        }

        public void ResetToDefault()
        {
            EnemiesCount = _startEnemiesCount - _enemiesWaveMultiplier;
        }
    }
}