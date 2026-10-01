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
    
    private TestTitleController _testTitleController;
    
    [SerializeField] private string _inGameSceneName;
    
    private void BindButtonEvents()
    {}
    
    private void UnbindButtonEvents()
    {}

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
        _testTitleController._titleUi.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
