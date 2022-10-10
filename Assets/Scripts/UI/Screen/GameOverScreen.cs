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
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private float _delayBeforeScreen;

    private void OnEnable()
    {
        _gates.Destroyed += OnGatesDestroyed;
        _retryButton.onClick.AddListener(RestartLevel);
        _menuButton.onClick.AddListener(LoadMenu);
    }

    protected override void Start()
    {
        base.Start();

        Close();
    }

    private void OnDisable()
    {
        _gates.Destroyed -= OnGatesDestroyed;
        _retryButton.onClick.RemoveListener(RestartLevel);
        _menuButton.onClick.RemoveListener(LoadMenu);
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