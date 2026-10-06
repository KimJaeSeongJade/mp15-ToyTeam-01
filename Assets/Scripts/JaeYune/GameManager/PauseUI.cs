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
    
    [SerializeField] private InGameUIController _inGameUIController;
    
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
    
    public void PressToContinue()
    {
        GameManager.Instance.ResumeGame();
    }

    private void PressToRestart()
    {
        _inGameUIController.ResumeBgm();
        GameManager.Instance.StartGame();
        GameManager.Instance.LoadScene(_restartScene);
    }

    private void PressToReturnMain()
    {
        _inGameUIController.ResumeBgm();
        GameManager.Instance.StartGame();
        GameManager.Instance.LoadScene(_returnMainScene);
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
