using UnityEngine;
using UnityEngine.UI;

public class TimeAccelerator : MonoBehaviour
{
    [SerializeField] private float _acceleratedTimeScale;
    [SerializeField] private Image _toggleImage;
    [SerializeField] private Sprite _x1SpeedSprite;
    [SerializeField] private Sprite _x2SpeedSprite;

    private bool isTimeAccelerated;

    private void OnEnable()
    {
        isTimeAccelerated = false;
        CheckTimeState();
    }

    public void SetTime()
    {
        isTimeAccelerated = !isTimeAccelerated;

        CheckTimeState();
    }

    private void CheckTimeState()
    {
        if (isTimeAccelerated)
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