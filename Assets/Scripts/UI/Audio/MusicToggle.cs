using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class MusicToggle : MonoBehaviour
{
    [SerializeField] private AudioController _musicController;
    [SerializeField] private Image _enabledSprite;
    [SerializeField] private Image _disabledSprite;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggleClick);
        CheckVolume();
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleClick);
    }

    private void OnToggleClick(bool toggleState)
    {
        _musicController.SetVolume();
        CheckVolume();
    }

    private void CheckVolume()
    {
        if (_musicController.VolumeState == false)
            Disable();
        else
            Enable();
    }

    private void Enable()
    {
        _disabledSprite.gameObject.SetActive(false);
        _enabledSprite.gameObject.SetActive(true);
    }

    private void Disable()
    {
        _enabledSprite.gameObject.SetActive(false);
        _disabledSprite.gameObject.SetActive(true);
    }
}
