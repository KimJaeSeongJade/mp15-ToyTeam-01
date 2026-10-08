using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    
    private bool _isReturn;
    private bool _isStart;
    
    private void Awake() => CacheComponents();
    private void OnEnable()
    {
        _isReturn = false;
        _isStart = false;
    }
    private void Update() => WaitForEndSound();
    
    private void WaitForEndSound()
    {
        if (!_isStart ||_audioSource.isPlaying || _audioSource.loop) 
            return;
        
        Stop();
    }
    
    private void CacheComponents()
    {
        _audioSource = GetComponent<AudioSource>();
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

    public SoundPlayer ConvertSourceToClip(AudioClip soundClip)
    {
        _audioSource.clip = soundClip;
        return this;
    }

    public void Play()
    {
        _audioSource.Play();
        _isStart = true;
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public void Resume()
    {
        _audioSource.UnPause();
    }

    public void Stop()
    {
        if (_isReturn)
            return;

        _isReturn = true;
        
        _audioSource.Stop();
        ReturnToList();
    }

    public void ReturnToList()
    {
        SoundManager.Instance.ReturnSoundToList(this);
    }
}
