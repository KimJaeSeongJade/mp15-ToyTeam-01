using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestInGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _pauseUI;
    
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _exitButton;

    private KeyCode _pauseKey = KeyCode.Q;
    private bool _isPausePressed => Input.GetKeyDown(_pauseKey);

    private void OnEnable() => BindButtons();
    private void OnDisable() => UnbindButtons();

    private void BindButtons()
    {
        _continueButton.onClick.AddListener(PauseKey);  
        _exitButton.onClick.AddListener(PauseKey);
    }

    private void BindGameFlow()
    {
    }
    
    private void UnbindButtons()
    {}
    
    private void UnbindGameFlow()
    {}
    
    private void Update()
    {
        
    }

    private void PauseKey()
    {
        if (!_isPausePressed)
            return;

        GameManager.Instance.PauseGame();
    }

}
