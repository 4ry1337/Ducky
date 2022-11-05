using UnityEngine;

public class AudioSystem : StaticInstance<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;
    public void PlaySound(AudioClip clip)
    {
        _effectSource.PlayOneShot(clip);
    }
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
}
