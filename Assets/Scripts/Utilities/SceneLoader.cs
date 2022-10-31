using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private Canvas _loadingScreen;
    [SerializeField] private Image _progressBarFiller;
    [SerializeField] private float _progressBarFillingSpeed = 3;
    [SerializeField] private int _progressBarUpdatePerSecond = 10;
    [SerializeField] private int _delayAfterSceneLoaded = 1;
    [SerializeField] private BackroundMusicController _musicController;

    private float _fillingTarget;
    private Scene _scene;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _progressBarUpdatePerSecond = 1000 / _progressBarUpdatePerSecond;
        _delayAfterSceneLoaded = 1000 * _delayAfterSceneLoaded;
    }

    private void Update()
    {
        _progressBarFiller.fillAmount = Mathf.MoveTowards(_progressBarFiller.fillAmount, _fillingTarget, _progressBarFillingSpeed * Time.deltaTime);
    }

    public async void LoadScene(string sceneName, LoadingType loadingType)
    {
        ResetProgressBar();

        var loadedScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _loadingScreen.gameObject.SetActive(true);

        do
        {
            await Task.Delay(_progressBarUpdatePerSecond);
            _fillingTarget = loadedScene.progress;
        }
        while (loadedScene.progress < 1);

        if (loadingType == LoadingType.WithDelay)
            await Task.Delay(_delayAfterSceneLoaded);

        _loadingScreen.gameObject.SetActive(false);

        SetLoadedSceneActive(sceneName);

        _musicController.SetTrack(_scene.name);
    }

    public void ChangeScene(string nextScene, string previousScene, LoadingType loadingType)
    {
        UnloadScene(previousScene);
        LoadScene(nextScene, loadingType);
    }

    private void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private void SetLoadedSceneActive(string sceneName)
    {
        _scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(_scene);
    }

    private void ResetProgressBar()
    {
        _fillingTarget = 0;
        _progressBarFiller.fillAmount = 0;
    }
}

public enum LoadingType
{
    WithDelay,
    WithoutDelay
}