using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : Screen
{
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _exitButton;

    public event Action OptionsButtonClicked;

    private void OnEnable()
    {
        OpenMenu();

        _optionsButton.onClick.AddListener(OnOptionsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
    }

    private void OnDisable()
    {
        _optionsButton?.onClick.RemoveListener(OnOptionsButtonClick);
        _exitButton?.onClick.RemoveListener(OnExitButtonClick);
    }

    public void OpenMenu() => Open();

    public void CloseMenu() => Close();

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
