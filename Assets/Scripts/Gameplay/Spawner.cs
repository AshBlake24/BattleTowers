using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Wave[] _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Gate _target;

    public event Action<int, int> EnemyKilled;
    public event Action WaveCleared;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private int _spawnedEnemies;
    private int _killedEnemies;
    private int _maxEnemiesInCurrentWave;

    private void Start()
    {
        if (_waves != null & _waves.Length > 0)
            SetWave(_currentWaveIndex);
    }

    private void Update()
    {
        if (_target.IsDestroyed)
        {
            StopCoroutine(SpawnEnemies());
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

        StartCoroutine(SpawnEnemies());
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

        enemy.Died += OnEnemyDied;
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _killedEnemies++;

        EnemyKilled?.Invoke(_killedEnemies, _maxEnemiesInCurrentWave);

        enemy.Died -= OnEnemyDied;
    }

    private Enemy GetRandomEnemy()
    {
        int enemyIndex = UnityEngine.Random.Range(0, _currentWave.Enemies.Length);

        return _currentWave.Enemies[enemyIndex];
    }

    private IEnumerator SpawnEnemies()
    {
        var spawnRate = new WaitForSeconds(_currentWave.SpawnRate);

        while (_spawnedEnemies < _currentWave.EnemiesCount)
        {
            InstantiateEnemy();

            _spawnedEnemies++;

            yield return spawnRate;
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