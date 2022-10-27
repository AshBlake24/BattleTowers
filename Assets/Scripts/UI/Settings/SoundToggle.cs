using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public abstract class SoundToggle : MonoBehaviour
{
    protected const string MusicVolume = "MusicVolume";
    protected const float MinVolume = -80f;
    protected const float MaxVolume = 0;


    [SerializeField] protected AudioMixer Mixer;
    [SerializeField] private Image _enabledSprite;
    [SerializeField] private Image _disabledSprite;

    private Toggle _toggle;

    private void Awake()
    {
        _toggle = GetComponent<Toggle>();
    }

    private void OnEnable()
    {
        _toggle.onValueChanged.AddListener(OnToggleClicked);

        OnToggleClicked(_toggle.isOn);
    }

    private void OnDisable()
    {
        _toggle.onValueChanged.RemoveListener(OnToggleClicked);
    }

    protected virtual void Enable()
    {
        _disabledSprite.gameObject.SetActive(false);
        _enabledSprite.gameObject.SetActive(true);
    }

    protected virtual void Disable()
    {
        _enabledSprite.gameObject.SetActive(false);
        _disabledSprite.gameObject.SetActive(true);
    }

    private void OnToggleClicked(bool toggleState)
    {
        if (toggleState)
            Enable();
        else
            Disable();
    }
}