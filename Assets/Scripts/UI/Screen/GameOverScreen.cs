using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : Screen
{
    [SerializeField] private Player _player;
    [SerializeField] private Button _restartButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private float _delayBeforeScreen;
    [SerializeField] private string _mainMenuSceneName;

    private void OnEnable()
    {
        _player.Died += OnDied;
        _restartButton.onClick.AddListener(RestartLevel);
    }

    protected override void Awake()
    {
        base.Awake();

        Close();
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
        _restartButton.onClick.RemoveListener(RestartLevel);
    }

    protected override void Open()
    {
        base.Open();

        _score.text = $"Score: {_player.Score}";
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDied()
    {
        StartCoroutine(EnableWithDelay());
    }

    private IEnumerator EnableWithDelay()
    {
        yield return Helpers.GetTime(_delayBeforeScreen);

        Open();
    }
}