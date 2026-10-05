using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _returnMainButton;
   
    [SerializeField] private string _restartScene;
    [SerializeField] private string _returnMainScene;
    
    private InGameUIController _inGameUIController;

    private void Awake()
    {
        gameObject.SetActive(false);
    }
    
    private void OnEnable()
    {
        BindButtons();
    }
    private void OnDisable()
    {
        UnbindButtons();
    }

    private void BindButtons()
    {
        _continueButton.onClick.AddListener(PressToContinue);  
        _restartButton.onClick.AddListener(PressToRestart);
        _returnMainButton.onClick.AddListener(PressToReturnMain);
    }

    private void UnbindButtons()
    {
        if (GameManager.Instance == null)
            return;
        
        _continueButton.onClick.RemoveListener(PressToContinue);
        _restartButton.onClick.RemoveListener(PressToRestart);
        _returnMainButton.onClick.RemoveListener(PressToReturnMain);
    }
    
    private void LateUpdate()
    {
        PauseKey();
    }

    public void PauseKey()
    {
        if (!_inGameUIController._isPausePressed)
            return;

        _inGameUIController.PauseBgm();
        OnGamePause();
        GameManager.Instance.PauseGame();
    }
    
    private void PressToContinue()
    {
        _inGameUIController.ResumeBgm();
        OnGameResume();
        GameManager.Instance.ResumeGame();
    }

    private void PressToRestart()
    {
        _inGameUIController.StopBgm();
        RestartGame();
        GameManager.Instance.LoadScene(_restartScene);
    }

    private void PressToReturnMain()
    {
        _inGameUIController.StopBgm();
        ReturnMain();
        GameManager.Instance.LoadScene(_returnMainScene);
    }

    private void OnGamePause()
    {
        gameObject.SetActive(true);
    }

    private void OnGameResume()
    {
        gameObject.SetActive(false);
    }

    private void RestartGame()
    {
        gameObject.SetActive(false);
    }

    private void ReturnMain()
    {
        gameObject.SetActive(false);
    }
}
