using UnityEngine;
using UnityEngine.UI;

public class StartWaveButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Spawner _spawner;

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
        _spawner.WaveCleared += OnWaveCleared;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
        _spawner.WaveCleared -= OnWaveCleared;
    }

    private void OnButtonClick()
    {
        _spawner.SetNextWave();
        _button.gameObject.SetActive(false);
    }

    private void OnWaveCleared()
    {
        _button.gameObject.SetActive(true);
    }
}