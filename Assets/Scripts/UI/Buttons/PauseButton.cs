using UnityEngine;

public class PauseButton : UnityEngine.UI.Extensions.Button
{
    [SerializeField] private PauseMenu _pauseMenu;

    protected override void OnButtonClick()
    {
        _pauseMenu.gameObject.SetActive(true);
    }
}