using UnityEngine;

public class MenuButton : UnityEngine.UI.Extensions.Button
{
    [SerializeField] private PauseMenu _pauseMenu;

    protected override void OnButtonClick()
    {
        _pauseMenu.gameObject.SetActive(true);
    }
}