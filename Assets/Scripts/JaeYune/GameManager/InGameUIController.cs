using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private GameObject _applyUis;
    [SerializeField] private GameObject _pauseUi;
    [SerializeField] private GameObject _clearUi;
    [SerializeField] private GameObject _gameOverUi;

    private KeyCode _pauseKey = KeyCode.Q;
    private bool _isPausePressed => Input.GetKeyDown(_pauseKey);
    
    private void Awake() => GameStart();

    private void OnEnable() => BindGameFlow();
    private void OnDisable() => UnbindGameFlow();

    private void BindGameFlow()
    {
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGamePause += ViewPause;
        GameManager.Instance.OnGameResume += ViewResume;
        GameManager.Instance.OnGameClear += ViewGameClear;
        GameManager.Instance.OnGameOver += ViewGameOver;

        _nexusData.OnHealthChanged += CheckNexusHealth;
    }

    private void UnbindGameFlow()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGamePause -= ViewPause;
        GameManager.Instance.OnGameResume -= ViewResume;
        GameManager.Instance.OnGameClear -= ViewGameClear;
        GameManager.Instance.OnGameOver -= ViewGameOver;
        
        _nexusData.OnHealthChanged -= CheckNexusHealth;
    }
    
    private void GameStart()
    {
        _isPause = false;
        _isGameEnd = false;
        
        _applyUis.SetActive(true);
        _pauseUi.SetActive(false);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameClear()
    {
        _isGameEnd = true;

        _clearScoreBoard.ShowResult(_stageData);
        
        _applyUis.SetActive(false);
        _clearUi.SetActive(true);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameOver()
    {
        _isGameEnd = true;
        
        _gameOverScoreBoard.ShowResult(_stageData);
        
        _applyUis.SetActive(false);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(true);
    }

    public void ViewPause()
    {
        _isPause = true;
        
        PauseBgm();
        _applyUis.SetActive(false);
        _pauseUi.SetActive(true);
    }

    public void ViewResume()
    {
        _isPause = false;
        
        ResumeBgm();
        _applyUis.SetActive(true);
        _pauseUi.SetActive(false);
    }
    
    // + 사운드
    [SerializeField] private AudioClip _inGameBgm;
    
    private SoundPlayer _bgm;

    private void Start()
    {
        PlayBgm();
        GameManager.Instance.StartGame();
    }

    private void OnDestroy() => StopBgm();

    public void PlayBgm()
    {
        _bgm = SoundManager.Instance.TakeSoundPlayer();
   
        if (_inGameBgm == null)
            return;
        
        _bgm.SetSoundVolume(0.3f)
            .SetSoundLoop(true)
            .PlaySoundWhenStart(true)
            .ConvertSourceToClip(_inGameBgm)
            .Play();
    }

    public void PauseBgm() => _bgm.Pause();
    public void ResumeBgm() => _bgm.Resume();

    public void StopBgm()
    {
        _bgm?.Stop();
        _bgm = null;
    }
    
    /// <summary>
    /// UI 발생 확인을 위한 임시 키 배정 및 임시 코드
    /// </summary>
    private KeyCode _gameClearKey = KeyCode.Keypad9;
    private KeyCode _gameOverKey = KeyCode.Keypad8;

    private bool _isGameClear => Input.GetKeyDown(_gameClearKey);

    private void Update()
    {
        if (_isGameEnd)
            return;

        if (_isPausePressed)
        {
            JudgePaused();
        }

        if (_isGameClear)
        {
            GameManager.Instance.ClearGame();
        }
    }
    
    // ++ 
    [SerializeField] private NexusData _nexusData;
    private void CheckNexusHealth(float health)
    {
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    // +++
    [SerializeField] private StageData _stageData;
    [SerializeField] private ScoreBoard _clearScoreBoard;
    [SerializeField] private ScoreBoard _gameOverScoreBoard;
    
    // ++++
    private bool _isPause;
    private bool _isGameEnd;

    private void JudgePaused()
    {
        if (_isPause)
        {
            GameManager.Instance.ResumeGame();
        }
        
        else
        {
            GameManager.Instance.PauseGame();
        }
    }
}
