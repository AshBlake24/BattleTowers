using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private Player _player;
    [SerializeField] private Gates _target;
    [SerializeField] private float _delayBeforeWave;

    public event Action<int, int> EnemyKilled;
    public event Action<float> WaveStarted;
    public event Action WaveCleared;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private int _maxEnemiesInCurrentWave;
    private Coroutine _spawnEnemies;


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
        Enemy enemy = Instantiate(GetRandomEnemy(), _spawnPoint);

        enemy.Init(_target);

        if (enemy.TryGetComponent(out WaypointFollower follower))
            follower.Init(_startWaypoint);

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

    private Enemy GetRandomEnemy()
    {
        int enemyIndex = UnityEngine.Random.Range(0, _currentWave.Enemies.Length);

        return _currentWave.Enemies[enemyIndex];
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