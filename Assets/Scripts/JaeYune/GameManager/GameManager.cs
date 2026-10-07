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
        Time.timeScale = 1;
        IsGameRunning = true;
        IsGameClear = false;
        OnGameStart?.Invoke();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsGameRunning = false;
        OnGamePause?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        IsGameRunning = true;
        OnGameResume?.Invoke();
    }

    public void ClearGame()
    {
        Time.timeScale = 0;
        IsGameRunning = false;
        IsGameClear = true;
        OnGameClear?.Invoke();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        IsGameRunning = false;
        OnGameOver?.Invoke();
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
