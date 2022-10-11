using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : Screen
{
    [SerializeField] private Button _pauseMenu;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _pauseMenu.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _restartButton.onClick.AddListener(OnRestartButtonClick);
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    protected override void Awake()
    {
        base.Awake();

        Close();
    }

    private void OnDisable()
    {
        _pauseMenu.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
        _restartButton.onClick.RemoveListener(OnRestartButtonClick);
        _menuButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    protected override void Open()
    {
        base.Open();

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
        throw new NotImplementedException();
    }
}