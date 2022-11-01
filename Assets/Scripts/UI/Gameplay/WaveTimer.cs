using System.Collections;
using UnityEngine;
using TMPro;

public class WaveTimer : MonoBehaviour
{
    private const int OneSecond = 1;

    [SerializeField] private Spawner _spawner;
    [SerializeField] private TMP_Text _timer;

    private void OnEnable()
    {
        _spawner.WaveStarted += OnWaveStarted;        
    }

    private void Start()
    {
        _timer.raycastTarget = false;
    }

    private void OnDisable()
    {
        _spawner.WaveStarted -= OnWaveStarted;
    }

    private void ChangeTime(float time)
    {
        _timer.text = string.Format("New wave in {0:0}...", time + OneSecond);
    }

    private void OnWaveStarted(float time)
    {
        _timer.gameObject.SetActive(true);

        StartCoroutine(StartCountdown(time));
    }

    private IEnumerator StartCountdown(float time)
    {
        while (time > 0)
        {
            ChangeTime(time);

            time--;

            yield return Helpers.GetTime(OneSecond);
        }

        ChangeTime(time);

        yield return Helpers.GetTime(OneSecond);

        _timer.gameObject.SetActive(false);
    }
}