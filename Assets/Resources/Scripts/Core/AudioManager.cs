using Resources.Scripts.Core;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _effectSource;
    [SerializeField] private AudioClip _audioClipButton;
    [SerializeField] private AudioClip _audioClickTrue;
    [SerializeField] private AudioClip _audioClickFalse;

    public void PlayAudio(AudioClip audioClip)
    {
        _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    
    public void PlayEffect(AudioClip effectClip)
    {
        _effectSource.clip = effectClip;
        _effectSource.Play();
    }
    
    public void PlayEffectTrue()
    {
        _effectSource.clip = _audioClickTrue;
        _effectSource.Play();
    }
    
    public void PlayEffectFalse()
    {
        _effectSource.clip = _audioClickFalse;
        _effectSource.Play();
    }
    public void PlayClickButton()
    {
        _effectSource.clip = _audioClipButton;
        _effectSource.Play();
    }
    
    public void StopAudio()
    {
        if (_audioSource.isPlaying)
        {
            _audioSource.Pause();
            _audioSource.clip = null;
        }
    }
    
    public void PlayAudio()
    {
        _audioSource.Play();
    }
}