using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;

    private void OnEnable()
    {
        Time.timeScale = 0f;
        _resumeButton.onClick.AddListener(OnResumeButtonClick);
    }

    private void OnDisable()
    {
        Time.timeScale = 1.0f;
        _resumeButton.onClick.RemoveListener(OnResumeButtonClick);
    }

    private void OnResumeButtonClick()
    {
        gameObject.SetActive(false);
    }
}