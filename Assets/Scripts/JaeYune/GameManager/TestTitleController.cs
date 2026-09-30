using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestTitleController : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _exitButton;
    //[SerializeField] private Button _creditsButton;
    
    [SerializeField] private string _inGameSceneName;

    private void OnEnable() => BindButtonEvents();
    private void OnDisable() => UnBindButtonEvents();
    
    private void BindButtonEvents()
    {
        _startButton.onClick.AddListener(StartGame);
        _exitButton.onClick.AddListener(ExitGame);
        //_creditsButton.onClick.AddListener();
    }
    
    private void UnBindButtonEvents()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _exitButton.onClick.RemoveListener(ExitGame);
        //_creditsButton.onClick.RemoveListener();
    }

    private void StartGame()
    {
        GameManager.Instance.LoadScene(_inGameSceneName);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
