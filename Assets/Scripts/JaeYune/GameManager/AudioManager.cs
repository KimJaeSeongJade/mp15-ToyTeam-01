using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private void Awake() => SetSingleton();
    AudioSource audioSource;
    
    
    
    
    
    
    
    
    
    private void Foo()
    {
        //audioSource.PlayOneShot(audioSource.clip);
        //AudioSource.PlayClipAtPoint(audioSource.clip, Vector3.zero);
    }
}
