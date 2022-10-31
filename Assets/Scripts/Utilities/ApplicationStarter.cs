using UnityEngine;

public class ApplicationStarter : MonoBehaviour
{
    [SerializeField] private AudioController _musicController;
    [SerializeField] private string _startScene;
    [SerializeField] private int _targetFrameRate = 60;
    [SerializeField] private LoadingType _loadingType;

    private void Start()
    {
        Application.targetFrameRate = _targetFrameRate;
        SceneLoader.Instance.LoadScene(_startScene, _loadingType);
        _musicController.SetVolume(_musicController.PlayOnAwake);
    }
}