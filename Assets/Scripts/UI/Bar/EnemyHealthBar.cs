using UnityEngine;

public class EnemyHealthBar : Bar
{
    [SerializeField] private Vector3 _rotationOffset;

    private Canvas _canvas;
    private Camera _camera;
    private Enemy _enemy;

    private void Start()
    {
        _camera = Helpers.Camera;
        _canvas = GetComponent<Canvas>();

        _canvas.worldCamera = _camera;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(_rotationOffset);
    }

    private void OnDisable()
    {
        if (_enemy != null)
            _enemy.HealthChanged -= OnValueChanged;
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;

        _enemy.HealthChanged += OnValueChanged;
    }
}