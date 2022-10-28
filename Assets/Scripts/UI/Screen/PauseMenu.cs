using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private TMP_Text _score;

    private void OnEnable()
    {
        _pauseButton.onClick.AddListener(OnPauseButtonClick);
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
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
        _homeButton.onClick.RemoveListener(OnMenuButtonClick);
    }

    protected override void Open()
    {
        base.Open();

        _score.text = $"Score: {_player.Score}";

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
        Time.timeScale = 1;
    }
}