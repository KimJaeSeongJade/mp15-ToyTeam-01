using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClear : MonoBehaviour
{
    [Header("클리어UI 내 버튼 설정부")]
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnMainButton;
    [Header("이동할 Scene 설정부")]
    [SerializeField] private string _restartSceneName;
    [SerializeField] private string _returnSceneName;
    
    private void OnEnable()
    {
        BindButtons();
        BindGameFlow();
    }
    private void OnDisable()
    {
        UnbindButtons();
        UnbindGameFlow();
    }

    private void BindButtons()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _returnMainButton.onClick.AddListener(ReturnMainMenu);
    }

    private void BindGameFlow()
    {
        GameManager.Instance.OnGameStart += RestartGame;
        GameManager.Instance.OnGameResume += ReturnMainMenu;
    }
    
    private void UnbindButtons()
    {
        _restartButton.onClick.RemoveListener(RestartGame);
        _returnMainButton.onClick.RemoveListener(ReturnMainMenu);
    }

    private void UnbindGameFlow()
    {
        GameManager.Instance.OnGameStart -= RestartGame;
        GameManager.Instance.OnGameResume -= ReturnMainMenu;
    }

    private void RestartGame()
    {
        GameManager.Instance.LoadScene(_restartSceneName);
    }

    private void ReturnMainMenu()
    {
        GameManager.Instance.LoadScene(_returnSceneName);
    }
}
