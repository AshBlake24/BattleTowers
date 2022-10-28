using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable Object/New Audio", fileName = "New Audio")]
public class AudioController : ScriptableObject
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private float _maxVolume;
    [SerializeField] private float _minVolume;
    [SerializeField] private string _exposedVolumeName;
    [SerializeField] private bool _playOnAwake;

    public bool PlayOnAwake => _playOnAwake;
    public bool VolumeState { get; private set; }

    public void SetVolume()
    {
        VolumeState = !VolumeState;

        CheckVolumeState();
    }

    public void SetVolume(bool volumeState)
    {
        VolumeState = volumeState;

        CheckVolumeState();
    }

    private void CheckVolumeState()
    {
        if (VolumeState == false)
            Disable();
        else
            Enable();
    }

    private void Enable()
    {
        _mixer.SetFloat(_exposedVolumeName, _maxVolume);
    }

    private void Disable()
    {
        _mixer.SetFloat(_exposedVolumeName, _minVolume);
    }
}