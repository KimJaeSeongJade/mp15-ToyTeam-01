using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _applyUis;
    [SerializeField] private GameObject _clearUi;
    [SerializeField] private GameObject _gameOverUi;

    private void Awake() => GameStart();

    private void OnEnable() => BindGameFlow();

    private void OnDisable() => UnbindGameFlow();

    private void BindGameFlow()
    {
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGameClear += ViewGameClear;
        GameManager.Instance.OnGameOver += ViewGameOver;
    }

    private void UnbindGameFlow()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGameClear -= ViewGameClear;
        GameManager.Instance.OnGameOver -= ViewGameOver;
    }
    
    private void GameStart()
    {
        _applyUis.SetActive(true);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameClear()
    {
        if (!_isGameClear) // 임시 코드
            return;
        
        _applyUis.SetActive(false);
        _clearUi.SetActive(true);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameOver()
    {
        if (!_isGameOver) // 임시 코드
            return;
        
        _applyUis.SetActive(false);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(true);
    }
    
    /// <summary>
    /// UI 발생 확인을 위한 임시 키 배정 및 임시 코드
    private KeyCode _gameClearKey = KeyCode.Alpha1;
    private KeyCode _gameOverKey = KeyCode.Alpha2;

    private bool _isGameClear => Input.GetKeyDown(_gameClearKey);
    private bool _isGameOver => Input.GetKeyDown(_gameOverKey);

    private void LateUpdate()
    {
        ViewGameClear();
        ViewGameOver();
    }
    /// </summary>
}
