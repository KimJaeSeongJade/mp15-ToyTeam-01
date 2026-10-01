using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectStage : MonoBehaviour
{
    [SerializeField] private Button _easyStageButton;
    [SerializeField] private Button _hardStageButton;
    [SerializeField] private Button _hellStageButton;
    [SerializeField] private Button _backToMainMenuButton;
    
    [SerializeField] private TestTitleController _testTitleController;
    
    [SerializeField] private string _inGameSceneName;

    private void OnEnable() => BindButtonEvents();

    private void OnDisable() => UnbindButtonEvents();

    private void BindButtonEvents()
    {
        _easyStageButton.onClick.AddListener(SelectEasyStageButton);
        _hardStageButton.onClick.AddListener(SelectHardStageButton);
        _hellStageButton.onClick.AddListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.AddListener(SelectBackToMainMenuButton);
    }

    private void UnbindButtonEvents()
    {
        _easyStageButton.onClick.RemoveListener(SelectEasyStageButton);
        _hardStageButton.onClick.RemoveListener(SelectHardStageButton);
        _hellStageButton.onClick.RemoveListener(SelectHellStageButton);
        _backToMainMenuButton.onClick.RemoveListener(SelectBackToMainMenuButton);
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
        _testTitleController.ViewMainMenu();
    }
}
