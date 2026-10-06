using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    
    [Header("UI 컨트롤러 설정부")]
    [SerializeField] private TitleController titleController;
    
    [Header("클릭 사운드 설정부")]
    [SerializeField] private AudioClip _clickSound2;
    
    private SoundPlayer _click2;
    
    private void OnEnable() => BindButtonEvents();

    private void OnDisable() => UnbindButtonEvents();

    private void BindButtonEvents()
    {
        _easyStageButton.onClick.AddListener(SelectEasyStageButton);
        _hardStageButton.onClick.AddListener(SelectHardStageButton);
        _hellStageButton.onClick.AddListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.AddListener(SelectBackToMainMenuButton);
        
        _easyStageButton.onClick.AddListener(ClickSounOn2);
        _hardStageButton.onClick.AddListener(ClickSounOn2);
        _hellStageButton.onClick.AddListener(ClickSounOn2);
        _backToMainMenuButton.onClick.AddListener(ClickSounOn2);
    }

    private void UnbindButtonEvents()
    {
        _easyStageButton.onClick.RemoveListener(SelectEasyStageButton);
        _hardStageButton.onClick.RemoveListener(SelectHardStageButton);
        _hellStageButton.onClick.RemoveListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.RemoveListener(SelectBackToMainMenuButton);
        
        _easyStageButton.onClick.RemoveListener(ClickSounOn2);
        _hardStageButton.onClick.RemoveListener(ClickSounOn2);
        _hellStageButton.onClick.RemoveListener(ClickSounOn2);
        _backToMainMenuButton.onClick.RemoveListener(ClickSounOn2);
    }

    public void SelectEasyStageButton()
    {
        GameManager.Instance.LoadScene(_inGameSceneName);
    }

    public void SelectHardStageButton()
    {
        GameManager.Instance.LoadScene(_inGameSceneName);
    }

    public void SelectHellStageButton()
    {
        GameManager.Instance.LoadScene(_inGameSceneName);
    }

    public void SelectBackToMainMenuButton()
    {
        titleController.ViewMainMenu();
    }
    
    private void ClickSounOn2()
    {
        _click2 = SoundManager.Instance.TakeSoundPlayer();
   
        if (_clickSound2 == null)
            return;
        
        _click2.SetSoundVolume(0.3f)
            .SetSoundLoop(false)
            .PlaySoundWhenStart(false)
            .ConvertSourceToClip(_clickSound2)
            .Play();
    }

    private void ClickSoundOff2()
    {
        _click2.Stop();
        _click2 = null;
    }
}
