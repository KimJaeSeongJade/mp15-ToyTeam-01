using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class InGameUIController : MonoBehaviour
{
    [Header("HUD 묶음 적용부")]
    [SerializeField] private GameObject _applyUis;
    
    [Header("게임 플로우 UI 관련 항목 적용부")]
    [SerializeField] private GameObject _pauseUi;
    [SerializeField] private GameObject _clearUi;
    [SerializeField] private GameObject _gameOverUi;

    [Header("일시정지 키 선택부")]
    [SerializeField] private KeyCode _pauseKey = KeyCode.Q;
    
    [Header("인게임 BGM 적용부")]
    [SerializeField] private AudioClip _inGameBgm;
    
    [Header("넥서스 데이터 적용부")]
    [SerializeField] private NexusData _nexusData;
    
    [Header("스테이지 설정 및 판단 관련 적용부")]
    [SerializeField] private StageData _stageData;
    [SerializeField] private ResultBoard _clearResultBoard;
    [SerializeField] private ResultBoard _gameOverResultBoard;
    
    private bool _isPausePressed => Input.GetKeyDown(_pauseKey);
    private SoundPlayer _bgm;
    private bool _isGameClear;
    private bool _isPause;
    private bool _isGameEnd;
    
    private void Awake() => GameStart();
    private void Start()
    {
        PlayBgm();
        GameManager.Instance.StartGame();
    }
    private void OnEnable() => BindGameFlow();
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
    private void OnDisable() => UnbindGameFlow();
    private void OnDestroy() => StopBgm();
    
    private void BindGameFlow()
    {
        GameManager.Instance.OnGameStart += GameStart;
        GameManager.Instance.OnGamePause += ViewPause;
        GameManager.Instance.OnGameResume += ViewResume;
        GameManager.Instance.OnGameClear += ViewGameClear;
        GameManager.Instance.OnGameOver += ViewGameOver;

        _nexusData.OnHealthChanged += CheckNexusHealth;
        
        _stageData.IsStageClear.OnValueChanged += CheckStageClear;
    }

    private void UnbindGameFlow()
    {
        GameManager.Instance.OnGameStart -= GameStart;
        GameManager.Instance.OnGamePause -= ViewPause;
        GameManager.Instance.OnGameResume -= ViewResume;
        GameManager.Instance.OnGameClear -= ViewGameClear;
        GameManager.Instance.OnGameOver -= ViewGameOver;
        
        _nexusData.OnHealthChanged -= CheckNexusHealth;
        
        _stageData.IsStageClear.OnValueChanged -= CheckStageClear;
    }
    
    private void GameStart()
    {
        _isPause = false;
        _isGameEnd = false;
        _isGameClear = false;
        
        Cursor.visible = false;
        _applyUis.SetActive(true);
        _pauseUi.SetActive(false);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameClear()
    {
        _isGameEnd = true;
        _isGameClear = true;

        _clearResultBoard.ShowResult(_stageData);
        
        Cursor.visible = true;
        _applyUis.SetActive(false);
        _clearUi.SetActive(true);
        _gameOverUi.SetActive(false);
    }

    public void ViewGameOver()
    {
        _isGameEnd = true;
        
        _gameOverResultBoard.ShowResult(_stageData);
        
        Cursor.visible = true;
        _applyUis.SetActive(false);
        _clearUi.SetActive(false);
        _gameOverUi.SetActive(true);
    }

    public void ViewPause()
    {
        _isPause = true;
        
        PauseBgm();
        Cursor.visible = true;
        _applyUis.SetActive(false);
        _pauseUi.SetActive(true);
    }

    public void ViewResume()
    {
        _isPause = false;
        
        ResumeBgm();
        Cursor.visible = false;
        _applyUis.SetActive(true);
        _pauseUi.SetActive(false);
    }
    
    // + 사운드
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
    
    // ++ 
    private void CheckNexusHealth(float health)
    {
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }
    
    // +++ 
    private void CheckStageClear(bool isStageClear)
    {
        _isGameClear = isStageClear;
    }

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
