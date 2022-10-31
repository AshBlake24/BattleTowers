using System;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenu : MonoBehaviour
{
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        _closeButton.onClick.AddListener(OnCloseButtonClick);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }

    private void OnCloseButtonClick()
    {
        gameObject.SetActive(false);
    }
}