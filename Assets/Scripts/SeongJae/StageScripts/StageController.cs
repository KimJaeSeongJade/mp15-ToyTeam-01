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

    private WaveData _currentWaveData;
    private StageData _stageData;
    private WaitForSeconds _waitForCoolDown;

    private bool _isTimeStopped;
    private bool _isDefeated;

    private bool _isRunning => !_stageData.IsClear && !_isDefeated;

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
            _waves[_stageData.CurrentWave].Clear();
            _waves[_stageData.CurrentWave].OnExit();
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
        _stageData.CurrentWave++;
        if (_stageData.CurrentWave >= _stageData.MaxWave)
        {
            Debug.Log($"{name} : 스테이지 클리어");
            _isTimeStopped = true;
            _stageData.IsClear = true;
            return;
        }
        _currentWaveData = _waves[_stageData.CurrentWave].GetComponent<WaveData>();
        _currentWaveData.OnDefeated += CheckDefeat;
        _waves[_stageData.CurrentWave].OnEnter();
    }
    private void Init()
    {
        _stageData.SetData(_waves.Count, _currentWaveData.AmountForClear, _timeForWave);
        _stageData.OnTimeChanged += CheckTimeOver;

        _currentWaveData = _waves[_stageData.CurrentWave].GetComponent<WaveData>();

        foreach (WaveController wave in _waves) wave.SetData(_stageData);

        _waves[_stageData.CurrentWave].OnEnter();
        _currentWaveData.OnDefeated += CheckDefeat;
        _currentWaveData.OnWaveCleared += CheckWaveClear;

        _stageData.OnStageCleared += TestStageClear;
    }

    private void CheckDefeat()
    {
        _isDefeated = true;
        _waves[_stageData.CurrentWave].OnExit();
        Debug.Log("패배");
    }

    private void CheckWaveClear()
    {
        _waves[_stageData.CurrentWave].OnExit();
        Debug.Log("웨이브 클리어");
        _stageData.Time = _timeForWave;
        StartCoroutine(WaveCoolRoutine());
    }

    private void CacheComponents()
    {
        _stageData = GetComponent<StageData>();
        _waitForCoolDown = new WaitForSeconds(_waveCoolDown);
        _isTimeStopped = false;
    }

    private void TestStageClear()
    {
        Debug.Log("스테이지 클리어 발생");
    }
}
