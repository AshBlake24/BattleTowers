using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class SoundToggle : MonoBehaviour
{
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

    private void OnToggleClicked(bool toggleState)
    {
        if (toggleState)
            Enable();
        else
            Disable();
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