using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    private void Awake() => Init();
    
    private void Init()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioClip = GetComponent<AudioSource>().clip;
    }
}
