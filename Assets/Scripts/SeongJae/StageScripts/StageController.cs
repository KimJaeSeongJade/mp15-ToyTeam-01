using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("사용할 웨이브 목록")]
    [SerializeField] private List<WaveController> _waves = new();

    [Header("웨이브 실행 주기")]
    [SerializeField] private float _waveCoolDown;

    [Header("한 웨이브 당 걸리는 시간")]
    [SerializeField] private float _timeForWave;

    private StageData _stageData;
    private WaitForSeconds _waitForCoolDown;

    private bool _isCleared;
    private bool _isTimeStopped;
    private bool _isDefeated;

    private bool _isRunning => !_isCleared && !_isDefeated;

    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update() => UpdateTime();

    public void UpdateTime()
    {
        if (_isTimeStopped) return;
        _stageData.Time -= Time.deltaTime;
    }

    public void CheckTimeOver(float time)
    {
        if(time <= 0 && _isRunning)
        {
            _waves[_stageData.CurrentWave].OnExit();
            _stageData.Time = _timeForWave;
            StartCoroutine(WaveCoolRoutine());
        }
    }

    private IEnumerator WaveCoolRoutine()
    {
        Debug.Log($"{_waveCoolDown} 초 후, 다음 웨이브 시작");
        _isTimeStopped = true;
        yield return _waitForCoolDown;
        _isTimeStopped = false;
        StartNextWave();
    }

    private void StartNextWave()
    {
        _waves[_stageData.CurrentWave].OnWaveDefeated -= SetDefeat;

        _stageData.CurrentWave++;
        if (_stageData.CurrentWave >= _stageData.MaxWave)
        {
            Debug.Log($"{name} : 스테이지 클리어");
            _isTimeStopped = true;
            _isCleared = true;
            return;
        }
        _waves[_stageData.CurrentWave].OnWaveDefeated += SetDefeat;
        _waves[_stageData.CurrentWave].OnEnter();
    }
    private void Init()
    {
        _stageData.CurrentWave = 0;
        _stageData.MaxWave = _waves.Count;
        _stageData.MonsterCount = _waves[_stageData.CurrentWave].MaxMonsterCount;
        _stageData.Score = 0;
        _stageData.Time = _timeForWave;
        _stageData.OnTimeChanged += CheckTimeOver;

        foreach(WaveController wave in _waves)
        {
            wave.SetNexus(_stageData.Nexus);
            wave.SetStageData(_stageData);
        }

        _waves[_stageData.CurrentWave].OnEnter();
        _waves[_stageData.CurrentWave].OnWaveDefeated += SetDefeat;
    }

    private void SetDefeat()
    {
        _isDefeated = true;
        Debug.Log($"{name} : 패배 확인, 게임 오버");
        _waves[_stageData.CurrentWave].OnExit();
    }

    private void CacheComponents()
    {
        _stageData = GetComponent<StageData>();
        _waitForCoolDown = new WaitForSeconds(_waveCoolDown);
        _isTimeStopped = false;
    }
}
