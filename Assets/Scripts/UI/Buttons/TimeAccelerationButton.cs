using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TimeAccelerationButton : MonoBehaviour
{
    [SerializeField] private TimeButton[] _timeButtons;
    [SerializeField] private Image _buttonImage;

    private Button _button;
    private int _currentTimeButonIndex;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        ResetTime();
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    public void ResetTime()
    {
        _currentTimeButonIndex = 0;
        ChangeTime(_timeButtons[_currentTimeButonIndex]);
    }

    private void ChangeTime(TimeButton timeButton)
    {
        Time.timeScale = timeButton.TimeSpeed;
        _buttonImage.sprite = timeButton.Icon;
    }

    private void OnButtonClick()
    {
        _currentTimeButonIndex++;

        if (_currentTimeButonIndex > _timeButtons.Length - 1)
            _currentTimeButonIndex = 0;

        ChangeTime(_timeButtons[_currentTimeButonIndex]);
    }

    [System.Serializable]
    private class TimeButton
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _timeSpeed;

        public Sprite Icon => _icon;
        public int TimeSpeed => _timeSpeed;
    }
}