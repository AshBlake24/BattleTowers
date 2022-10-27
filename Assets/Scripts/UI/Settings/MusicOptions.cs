public class MusicOptions : SoundToggle
{
    protected override void Enable()
    {
        base.Enable();
        Mixer.SetFloat(MusicVolume, MaxVolume);
    }

    protected override void Disable()
    {
        base.Disable();
        Mixer.SetFloat(MusicVolume, MinVolume);
    }
}