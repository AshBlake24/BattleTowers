using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : Screen
{
    [SerializeField] private Gates _gates;
    [SerializeField] private Player _player;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _homeButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private float _delayBeforeScreen;

    private void OnEnable()
    {
        _gates.Destroyed += OnGatesDestroyed;
        _restartButton.onClick.AddListener(RestartLevel);
        _homeButton.onClick.AddListener(LoadMenu);
    }

    protected override void Awake()
    {
        base.Awake();

        Close();
    }

    private void OnDisable()
    {
        _gates.Destroyed -= OnGatesDestroyed;
        _restartButton.onClick.RemoveListener(RestartLevel);
        _homeButton.onClick.RemoveListener(LoadMenu);
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

    private void LoadMenu()
    {
        throw new NotImplementedException();
    }

    private void OnGatesDestroyed()
    {
        StartCoroutine(DelayEnable());
    }

    private IEnumerator DelayEnable()
    {
        yield return Helpers.GetTime(_delayBeforeScreen);

        Open();
    }
}