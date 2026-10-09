using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [Header("버튼 설정부")]
    [SerializeField] private Button _easyStageButton;
    [SerializeField] private Button _hardStageButton;
    [SerializeField] private Button _hellStageButton;
    [SerializeField] private Button _backToMainMenuButton;
    
    [Header("이동할 Scene 설정부")]
    [SerializeField] private string _inGameSceneName;
    [SerializeField] private string _easySceneName;
    [SerializeField] private string _hardSceneName;
    
    [Header("UI 컨트롤러 설정부")]
    [SerializeField] private TitleController titleController;
    
    [Header("클릭 사운드 설정부")]
    [SerializeField] private AudioClip _clickSound;
    
    private SoundPlayer _click;
    
    private void OnEnable() => BindButtonEvents();

    private void OnDisable() => UnbindButtonEvents();

    private void BindButtonEvents()
    {
        _easyStageButton.onClick.AddListener(SelectEasyStageButton);
        _hardStageButton.onClick.AddListener(SelectHardStageButton);
        _hellStageButton.onClick.AddListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.AddListener(SelectBackToMainMenuButton);
        
        _easyStageButton.onClick.AddListener(ClickSounOn);
        _hardStageButton.onClick.AddListener(ClickSounOn);
        _hellStageButton.onClick.AddListener(ClickSounOn);
        _backToMainMenuButton.onClick.AddListener(ClickSounOn);
    }

    private void UnbindButtonEvents()
    {
        _easyStageButton.onClick.RemoveListener(SelectEasyStageButton);
        _hardStageButton.onClick.RemoveListener(SelectHardStageButton);
        _hellStageButton.onClick.RemoveListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.RemoveListener(SelectBackToMainMenuButton);
        
        _easyStageButton.onClick.RemoveListener(ClickSounOn);
        _hardStageButton.onClick.RemoveListener(ClickSounOn);
        _hellStageButton.onClick.RemoveListener(ClickSounOn);
        _backToMainMenuButton.onClick.RemoveListener(ClickSounOn);
    }

    public void SelectEasyStageButton()
    {
        GameManager.Instance.LoadScene(_easySceneName);
    }

    public void SelectHardStageButton()
    {
        GameManager.Instance.LoadScene(_hardSceneName);
    }

    public void SelectHellStageButton()
    {
        GameManager.Instance.LoadScene(_inGameSceneName);
    }

    public void SelectBackToMainMenuButton()
    {
        titleController.ViewMainMenu();
    }
    
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
}
