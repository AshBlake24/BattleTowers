using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private string _mainMenuSceneName;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _homeButton.onClick.AddListener(OnMenuButtonClick);
    }

    protected override void Awake()
    {
        base.Awake();

        Close();
    }

    private void OnDisable()
    {
        _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _homeButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    protected override void Open()
    {
        base.Open();

        _score.text = _player.Score.ToString();

        Time.timeScale = 0;
    }

    protected override void Close()
    {
        base.Close();

        Time.timeScale = 1;
    }

    private void OnPauseButtonClick() => Open();

    private void OnResumeButtonClick() => Close();

    private void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnMenuButtonClick()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }
}