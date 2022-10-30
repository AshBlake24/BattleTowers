using UnityEngine;

public class ApplicationStarter : MonoBehaviour
{
    [SerializeField] private AudioController _musicController;
    [SerializeField] private string _startScene;
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private bool _loadWithDelay;

    private void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
        SceneLoader.Instance.LoadScene(_startScene, _loadWithDelay);
        _musicController.SetVolume(_musicController.PlayOnAwake);
    }
}