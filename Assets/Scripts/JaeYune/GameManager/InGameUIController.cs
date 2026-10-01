using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _applyUis;
    [SerializeField] private GameObject _clearUi;
    [SerializeField] private GameObject _gameOverUi;

    private void Awake() => StartGame();
    
    public void StartGame()
    {
        GameManager.Instance.StartGame();
        _applyUis.SetActive(true);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(false);
    }

    public void GameClear()
    {
        GameManager.Instance.ClearGame();
        _applyUis.SetActive(false);
        _clearUi.SetActive(true);
        _gameOverUi.SetActive(false);
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
        _applyUis.SetActive(false);
        _clearUi.SetActive(true);
        _gameOverUi.SetActive(false);
    }
}
