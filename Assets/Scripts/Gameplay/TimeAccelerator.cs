using UnityEngine;
using UnityEngine.UI;

public class TimeAccelerator : MonoBehaviour
{
    [SerializeField] private float _acceleratedTimeScale;
    [SerializeField] private Image _toggleImage;
    [SerializeField] private Sprite _x1SpeedSprite;
    [SerializeField] private Sprite _x2SpeedSprite;

    private bool _isTimeAccelerated;

    private void OnEnable()
    {
        _isTimeAccelerated = false;
        CheckTimeState();
    }

    public void SetTime()
    {
        _isTimeAccelerated = !_isTimeAccelerated;

        CheckTimeState();
    }

    public void SetTime(bool isTimeAccelerated)
    {
        _isTimeAccelerated = isTimeAccelerated;

        CheckTimeState();
    }

    private void CheckTimeState()
    {
        if (_isTimeAccelerated)
            Enable();
        else
            Disable();
    }

    private void Enable()
    {
        _toggleImage.sprite = _x2SpeedSprite;
        Time.timeScale = _acceleratedTimeScale;
    }

    private void Disable()
    {
        _toggleImage.sprite = _x1SpeedSprite;
        Time.timeScale = 1.0f;
    }
}