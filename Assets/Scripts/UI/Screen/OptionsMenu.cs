using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : Screen
{
    [SerializeField] private MainMenu _mainMenu;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        Close();

        _mainMenu.OptionsButtonClicked += OnOptionsButtonClick;
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void OnDisable()
    {
        _mainMenu.OptionsButtonClicked -= OnOptionsButtonClick;
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnOptionsButtonClick() => Open();

    private void OnCloseButtonClick()
    {
        Close();
        _mainMenu.OpenMenu();
    }
}