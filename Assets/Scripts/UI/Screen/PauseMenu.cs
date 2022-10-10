using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Screen
{
    [SerializeField] private Button _pauseMenu;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _menuButton;

    private void OnEnable()
    {
        _pauseMenu.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
        _menuButton.onClick.AddListener(OnMenuButtonClick);
    }

    protected override void Start()
    {
        base.Start();

        Close();
    }

    private void OnDisable()
    {
        _pauseMenu.onClick.RemoveListener(OnPauseButtonClick);
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
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

    private void OnMenuButtonClick()
    {
        throw new NotImplementedException();
    }
}