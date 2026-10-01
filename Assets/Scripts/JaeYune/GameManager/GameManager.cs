using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action OnGameStart;
    public event Action OnGamePause;
    public event Action OnGameResume;
    public event Action OnGameClear;
    public event Action OnGameOver;
    
    public bool IsGameRunning { get; private set; }
    public bool IsGameClear { get; private set; }

    private void Awake() => SetSingleton();

    public void StartGame()
    {
        OnGameStart?.Invoke();
        Time.timeScale = 1;
        IsGameRunning = true;
        IsGameClear = false;
    }

    public void PauseGame()
    {
        OnGamePause?.Invoke();
        Time.timeScale = 0;
        IsGameRunning = false;
    }

    public void ResumeGame()
    {
        OnGameResume?.Invoke();
        Time.timeScale = 1;
        IsGameRunning = true;
    }

    public void ClearGame()
    {
        OnGameClear?.Invoke();
        Time.timeScale = 0;
        IsGameRunning = false;
        IsGameClear = true;
    }

    public void GameOver()
    {
        OnGameOver?.Invoke();
        Time.timeScale = 0;
        IsGameRunning = false;
    }

    public void RestartGame()
    {
        IsGameClear = false;
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
