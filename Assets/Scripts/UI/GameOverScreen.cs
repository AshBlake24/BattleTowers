using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Gates _gates;
    [SerializeField] private Player _player;
    [SerializeField] private Button _retryButton;
    [SerializeField] private Button _menuButton;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private float _delayBeforeScreen;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _gates.Destroyed += OnGatesDestroyed;
        _retryButton.onClick.AddListener(RestartLevel);
        _menuButton.onClick.AddListener(LoadMenu);
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        Disable();
    }

    private void OnDisable()
    {
        _gates.Destroyed -= OnGatesDestroyed;
        _retryButton.onClick.RemoveListener(RestartLevel);
        _menuButton.onClick.RemoveListener(LoadMenu);
    }

    private void Enable()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;

        _score.text = $"Score: {_player.Score}";
    }

    private void Disable()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
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

        Enable();
    }
}