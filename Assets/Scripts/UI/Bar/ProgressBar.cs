using UnityEngine;

public class ProgressBar : Bar
{
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _spawner.EnemyKilled += OnValueChanged;
    }

    private void OnDisable()
    {
        _spawner.EnemyKilled -= OnValueChanged;
    }
}