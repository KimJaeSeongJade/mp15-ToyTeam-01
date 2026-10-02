using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [SerializeField] private GameObject _titleUi;
    [SerializeField] private GameObject _selectStageUi;
    [SerializeField] private AudioClip _selectBgm;
    
    private SoundPlayer _bgm;
    
    private void Awake() => ViewMainMenu();
    private void Start() => PlayBgm();
    
    private void OnEnable() => BindGameFlow();
    private void OnDisable() => UnbindGameFlow();

    private void OnDestroy() => StopBgm();

    private void BindGameFlow()
    {
        GameManager.Instance.OnGameResume += ViewMainMenu;
    }

    private void UnbindGameFlow()
    {
        GameManager.Instance.OnGameResume -= ViewMainMenu;
    }
    
    public void ViewSelectStage()
    {
        _titleUi.SetActive(false);
        _selectStageUi.SetActive(true);
    }

    public void ViewMainMenu()
    {
        _titleUi.SetActive(true);
        _selectStageUi.SetActive(false);
    }
    
    // + 사운드 관련 추가중
    private void PlayBgm()
    {
        _bgm = SoundManager.Instance.TakeSoundPlayer();
   
        if (_selectBgm == null)
            return;
        
        _bgm.SetSoundVolume(0.3f)
            .SetSoundLoop(true)
            .PlaySoundWhenStart(true)
            .ConvertSourceToClip(_selectBgm)
            .Play();
    }

    private void StopBgm()
    {
        _bgm.Stop();
        _bgm = null;
    }
}
