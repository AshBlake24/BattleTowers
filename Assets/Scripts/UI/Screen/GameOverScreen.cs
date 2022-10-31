using System.Collections;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _menu;
    [SerializeField] private float _delayBeforeScreen;

    private void Awake()
    {
        _menu.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _player.Died += OnDied;
    }

    private void OnDisable()
    {
        _player.Died -= OnDied;
    }

    private void OnDied()
    {
        StartCoroutine(EnableWithDelay());
    }

    private IEnumerator EnableWithDelay()
    {
        yield return Helpers.GetTime(_delayBeforeScreen);

        _menu.gameObject.SetActive(true);
    }
}