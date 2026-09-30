using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;
    [SerializeField] private Button _continueButton;

    private KeyCode _pauseKey = KeyCode.Q;
    private bool _isPausePressed => Input.GetKeyDown(_pauseKey);

    private void Awake()
    {
        _pauseUI.SetActive(false);
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
    }

    private void UnbindButtons()
    {
        if (GameManager.Instance == null)
            return;
        
        _continueButton.onClick.RemoveListener(PressToContinue);
    }
    
    private void LateUpdate()
    {
        PauseKey();
    }

    private void PauseKey()
    {
        if (!_isPausePressed)
            return;

        OnGamePause();
        GameManager.Instance.PauseGame();
    }
    
    private void PressToContinue()
    {
        OnGameResume();
        GameManager.Instance.ResumeGame();
    }

    private void OnGamePause()
    {
        _pauseUI.SetActive(true);
    }

    private void OnGameResume()
    {
        _pauseUI.SetActive(false);
    }
}
