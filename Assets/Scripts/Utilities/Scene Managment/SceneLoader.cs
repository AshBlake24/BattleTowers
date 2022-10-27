using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _progressBarFillingSpeed = 3;
    [SerializeField] private int _progressBarUpdatePerSecond = 10;
    [SerializeField] private int _delayAfterSceneLoaded = 1;

    private float _fillingTarget;

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
        _progressBar.fillAmount = Mathf.MoveTowards(_progressBar.fillAmount, _fillingTarget, _progressBarFillingSpeed * Time.deltaTime);
    }

    public async void LoadScene(string sceneName)
    {
        ResetProgressBar();

        var loadedScene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        _loadingScreen.SetActive(true);

        do
        {
            await Task.Delay(_progressBarUpdatePerSecond);
            _fillingTarget = loadedScene.progress;
        }
        while (loadedScene.progress < 1);

        await Task.Delay(_delayAfterSceneLoaded);

        _loadingScreen.SetActive(false);

        SetLoadedSceneActive(sceneName);
    }

    public void ChangeScene(string nextScene, string previousScene)
    {
        UnloadScene(previousScene);
        LoadScene(nextScene);
    }

    private void UnloadScene(string sceneName)
    {
        SceneManager.UnloadSceneAsync(sceneName);
    }

    private void SetLoadedSceneActive(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(scene);
    }

    private void ResetProgressBar()
    {
        _fillingTarget = 0;
        _progressBar.fillAmount = 0;
    }
}
