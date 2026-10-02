using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake() => CacheComponents();
    private void Update() => WaitForEndSound();
    private void CacheComponents()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void WaitForEndSound()
    {
        if (_audioSource.isPlaying || _audioSource.loop) 
            return;
        
        Stop();
    }
    
    public SoundPlayer SetSoundVolume(float soundVolume)
    {
        _audioSource.volume = soundVolume;
        return this;
    }

    public SoundPlayer SetSoundLoop(bool isLoop)
    {
        _audioSource.loop = isLoop;
        return this;
    }

    public SoundPlayer PlaySoundWhenStart(bool isPlay)
    {
        _audioSource.playOnAwake = isPlay;
        return this;
    }

    public SoundPlayer MakeSourceToClip(AudioClip soundClip)
    {
        _audioSource.clip = soundClip;
        return this;
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Stop()
    {
        _audioSource.Stop();
        ReturnToList();
    }

    public void ReturnToList()
    {
        AudioManager.Instance.ReturnSoundToList(this);
    }
}
