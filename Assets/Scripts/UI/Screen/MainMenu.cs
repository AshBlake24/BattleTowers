using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : Screen
{
    [SerializeField] private string _levelSceneName;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    public event Action OptionsButtonClicked;

    private void OnEnable()
    {
        OpenMenu();

        _playButton.onClick.AddListener(OnPlayButtonClick);
        _optionsButton.onClick.AddListener(OnOptionsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _optionsButton?.onClick.RemoveListener(OnOptionsButtonClick);
        _exitButton?.onClick.RemoveListener(OnExitButtonClick);
    }

    public void OpenMenu() => Open();

    public void CloseMenu() => Close();

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene(_levelSceneName);
    }

    private void OnOptionsButtonClick()
    {
        Close();
        OptionsButtonClicked?.Invoke();
    }

    private void OnExitButtonClick()
    {
        Application.Quit();
    }
}
