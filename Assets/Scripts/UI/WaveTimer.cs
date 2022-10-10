using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CanvasGroup))]
public class WaveTimer : MonoBehaviour
{
    private const string DisableMethod = "Disable";
    private const int OneSecond = 1;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _timer;

    private CanvasGroup _canvasGroup;

    private void OnEnable()
    {
        _spawner.WaveStarted += OnWaveStarted;
    }

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnDisable()
    {
        _spawner.WaveStarted -= OnWaveStarted;
    }

    private void Enable()
    {
        _canvasGroup.alpha = 1.0f;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }

    private void Disable()
    {
        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;
    }

    private void ChangeTime(float time)
    {
        _timer.text = string.Format("New wave in {0:0}...", time + OneSecond);
    }

    private void OnWaveStarted(float time) => StartCoroutine(StartCountdown(time));

    private IEnumerator StartCountdown(float time)
    {
        Enable();

        while (time > 0)
        {
            ChangeTime(time);

            time--;

            yield return Helpers.GetTime(OneSecond);
        }

        ChangeTime(time);

        Invoke(DisableMethod, OneSecond);
    }
}
