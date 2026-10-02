using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    //[SerializeField] private Button _creditsButton;
    
    [SerializeField] private TitleController titleController;
    
    private void OnEnable() => BindButtonEvents();
    private void OnDisable() => UnBindButtonEvents();

    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(ClickSounOn);
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ClickSounOn);
        _exitButton.onClick.AddListener(ExitGame);
        //_creditsButton.onClick.AddListener();
    }
    
    private void UnBindButtonEvents()
    {
        _startButton.onClick.RemoveListener(ClickSounOn);
        _startButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(ClickSounOn);
        _exitButton.onClick.RemoveListener(ExitGame);
        //_creditsButton.onClick.RemoveListener();
    }
    
    private void StartGame()
    {
        titleController.ViewSelectStage();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
    
    // + 
    [SerializeField] private AudioClip _clickSound;
    private SoundPlayer _click;
    
    private void ClickSounOn()
    {
        _click = SoundManager.Instance.TakeSoundPlayer();
   
        if (_clickSound == null)
            return;
        
        _click.SetSoundVolume(0.3f)
            .SetSoundLoop(false)
            .PlaySoundWhenStart(false)
            .ConvertSourceToClip(_clickSound)
            .Play();
    }

    private void ClickSoundOff()
    {
        _click.Stop();
        _click = null;
    }
}
