using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _effectSource;
    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.Play();
    }
    public void PlaySound(AudioClip clip, Vector3 pos, float vol = 1f)
    {
        _effectSource.transform.position = pos;
        PlaySound(clip, vol);
    }
    public void PlaySound(AudioClip clip, float vol = 1f)
    {
        _effectSource.PlayOneShot(clip, vol);
    }
}
