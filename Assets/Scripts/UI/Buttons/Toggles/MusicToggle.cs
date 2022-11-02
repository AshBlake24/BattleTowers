using UnityEngine;

public class MusicToggle : UnityEngine.UI.Extensions.Toggle
{
    [SerializeField] private AudioController _musicController;

    protected override void OnEnable()
    {
        base.OnEnable();
        CheckVolume();
    }

    protected override void OnToggleClick(bool toggleState)
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
}