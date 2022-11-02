using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private Wave _wave;
    [SerializeField] private SpawnPoint[] _spawnPoints;
    [SerializeField] private float _delayBeforeWave;

    [Header("Target Settings")]
    [SerializeField] private Player _player;

    public event Action<int, int> EnemyKilled;
    public event Action<float> WaveStarted;
    public event Action WaveCleared;

    private int _currentWaveNumber;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private Coroutine _spawnEnemies;

    private void Start()
    {
        _currentWaveNumber = 0;
        _wave.ResetToDefault();
    }

    private void Update()
    {
        if (_player.IsAlive == false)
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

    public void SetNextWave()
    {
        _currentWaveNumber++;
        SetWave();
    }

    private void SetWave()
    {
        _wave.SetDifficulty(_currentWaveNumber);
        _killedEnemies = 0;

        StartCoroutine(LaunchWave());
    }

    private void ResetWave()
    {
        _spawnedEnemies = 0;
    }

    private void InstantiateEnemy()
    {
        SpawnPoint spawnPoint = GetRandomSpawnPoint();
        Enemy enemy = Instantiate(GetRandomEnemy(), spawnPoint.transform);

        if (enemy.TryGetComponent(out WaypointFollower follower))
            follower.Init(spawnPoint.StartWaypoint);

        var enemyHealthBar = enemy.GetComponentInChildren<EnemyHealthBar>();

        if (enemyHealthBar != null)
            enemyHealthBar.Init(enemy);

        enemy.Init(_player);

        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy, bool killedByPlayer)
    {
        _killedEnemies++;

        if (killedByPlayer)
        {
            _player.AddMoney(enemy.Reward);
            _player.AddScore(enemy.Score);
        }

        EnemyKilled?.Invoke(_killedEnemies, _wave.EnemiesCount);

        enemy.Died -= OnEnemyDied;
    }

    private Enemy GetRandomEnemy()
    {
        int enemyIndex = UnityEngine.Random.Range(0, _wave.Enemies.Length);
        return _wave.Enemies[enemyIndex];
    }

    private SpawnPoint GetRandomSpawnPoint()
    {
        int spawnIndex = UnityEngine.Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[spawnIndex];
    }

    private IEnumerator LaunchWave()
    {
        WaveStarted?.Invoke(_delayBeforeWave);
        EnemyKilled?.Invoke(_killedEnemies, _wave.EnemiesCount);

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
        [Header("Wave Settings")]
        [SerializeField] private Enemy[] _enemies;
        [SerializeField] private int _startEnemiesCount;
        [SerializeField] private int _enemiesWaveMultiplier;

        [Header("Spawn Rate Settings")]
        [SerializeField] private float[] _spawnRates;

        public Enemy[] Enemies => GetEnemies();
        public float SpawnRate => GetSpawnRate();
        public int EnemiesCount { get; private set; }

        public void SetDifficulty(int waveNumber)
        {
            EnemiesCount += waveNumber * _enemiesWaveMultiplier;
        }

        public void ResetToDefault()
        {
            EnemiesCount = _startEnemiesCount - _enemiesWaveMultiplier;
        }

        public float GetSpawnRate()
        {
            int timeIndex = UnityEngine.Random.Range(0, _spawnRates.Length);
            return _spawnRates[timeIndex];
        }

        private Enemy[] GetEnemies()
        {
            Enemy[] enemies = new Enemy[_enemies.Length];

            _enemies.CopyTo(enemies, 0);

            return enemies;
        }
    }
}