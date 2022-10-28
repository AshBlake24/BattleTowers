using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackroundMusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] _music;

    private AudioSource _audioSource;

    private void OnEnable()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetTrack(string sceneName)
    {
        Stop();

        foreach (var music in _music)
        {
            if (sceneName == music.name)
            {
                _audioSource.clip = music;
                Play();
                return;
            }
        }
    }

    private void Stop()
    {
        _audioSource.Stop();
    }

    private void Play()
    {
        _audioSource.Play();
    }
}