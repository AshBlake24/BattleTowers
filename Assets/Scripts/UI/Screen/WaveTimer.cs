using System.Collections;
using UnityEngine;
using TMPro;

public class WaveTimer : Screen
{
    private const string CloseMethod = "Close";
    private const int OneSecond = 1;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _timer;

    private void OnEnable()
    {
        _spawner.WaveStarted += OnWaveStarted;
    }

    private void OnDisable()
    {
        _spawner.WaveStarted -= OnWaveStarted;
    }

    protected override void Open()
    {
        CanvasGroup.alpha = 1.0f;
    }

    protected override void Close()
    {
        CanvasGroup.alpha = 0f;
    }

    private void ChangeTime(float time)
    {
        _timer.text = string.Format("New wave in {0:0}...", time + OneSecond);
    }

    private void OnWaveStarted(float time) => StartCoroutine(StartCountdown(time));

    private IEnumerator StartCountdown(float time)
    {
        Open();

        while (time > 0)
        {
            ChangeTime(time);

            time--;

            yield return Helpers.GetTime(OneSecond);
        }

        ChangeTime(time);

        Invoke(CloseMethod, OneSecond);
    }
}