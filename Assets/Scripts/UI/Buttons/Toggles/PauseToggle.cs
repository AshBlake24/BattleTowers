using UnityEngine;

public class PauseToggle : UnityEngine.UI.Extensions.Toggle
{
    [SerializeField] private Canvas _pauseVignette;

    private float _previousTimeScale;

    protected override void OnEnable()
    {
        base.OnEnable();
        SetPreviousTimeScale();
        CheckPause(ToggleComponent.isOn);
    }

    protected override void OnToggleClick(bool isGamePaused)
    {
        CheckPause(isGamePaused);
    }

    protected override void Enable()
    {
        base.Enable();
        _pauseVignette.gameObject.SetActive(true);
        SetPreviousTimeScale();
        Time.timeScale = 0;
    }

    protected override void Disable()
    {
        base.Disable();
        Time.timeScale = _previousTimeScale;
        _pauseVignette.gameObject.SetActive(false);
    }

    public void ResetToggle()
    {
        ToggleComponent.isOn = false;
        CheckPause(ToggleComponent.isOn);
    }

    private void CheckPause(bool isGamePaused)
    {
        if (isGamePaused)
            Enable();
        else
            Disable();
    }

    private void SetPreviousTimeScale()
    {
        _previousTimeScale = Time.timeScale;
    }
}