using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private SoundPlayer _soundPlayerPrefab;
    [SerializeField] private int _trackSize;
    
    private Stack<SoundPlayer> _soundPlayerList;
    public bool IsInitializeFinish { get; private set; }
    
    private void Awake()
    {
        SetSingleton();
        InitializeSoundList();
    }

    public SoundPlayer TakeSoundPlayer()
    {
        if (!IsInitializeFinish)
        {
            Debug.Log("사운드 리스트가 초기화 되지 않았습니다.");
            return null;
        }

        SoundPlayer soundPlayer;

        if (_soundPlayerList.Count == 0)
        {
            soundPlayer = CreateSoundPlayer();
        }
        
        else
        {
            soundPlayer = _soundPlayerList.Pop();
        }
        
        soundPlayer.gameObject.SetActive(true);

        return soundPlayer;
    }

    private void InitializeSoundList()
    {
        _soundPlayerList = new Stack<SoundPlayer>(_trackSize);

        while (_soundPlayerList.Count < _trackSize)
        {
            _soundPlayerList.Push(CreateSoundPlayer());
        }
        
        IsInitializeFinish = true;
    }

    private SoundPlayer CreateSoundPlayer()
    {
        SoundPlayer sound = Instantiate(_soundPlayerPrefab);
        sound.transform.SetParent(transform);
        sound.gameObject.SetActive(false);
        
        return sound;
    }

    public void ReturnSoundToList(SoundPlayer soundPlayer)
    {
        _soundPlayerList.Push(soundPlayer);
        soundPlayer.gameObject.SetActive(false);
    }
}
