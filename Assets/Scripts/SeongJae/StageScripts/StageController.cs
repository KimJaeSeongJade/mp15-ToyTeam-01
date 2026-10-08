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
    private void Awake() => CacheComponents();
    private void Start() => Init();
    private void Update() => RefreshTime();
    private void RefreshTime()
    {
        if (_isTimeStopped) return;

        _stageData.Time.Value -= Time.deltaTime;
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
        _stageData.CurrentWave.Value++;
        if (_stageData.CurrentWave.Value >= _stageData.MaxWave.Value)
        {
            Debug.Log($"{name} : 스테이지 클리어");
            _isTimeStopped = true;
            _stageData.IsStageClear.Value = true;
            return;
        }
        _currentWaveData = _waves[_stageData.CurrentWave.Value].GetComponent<WaveData>();

        _currentWaveData.IsWaveClear.OnValueChanged += CheckWaveClear;
        _waves[_stageData.CurrentWave.Value].OnEnter();
    }
    private void Init()
    {
        _currentWaveData = _waves[_stageData.CurrentWave.Value].GetComponent<WaveData>();
        _stageData.SetData(_waves.Count, _currentWaveData.AmountForClear, _timeForWave);
        _stageData.NexusData.OnHealthChanged += CheckDefeat;

        foreach(WaveController wave in _waves)
        {
            wave.SetData(_stageData);
        }

        _waves[_stageData.CurrentWave.Value].OnEnter();
        _currentWaveData.IsWaveClear.OnValueChanged += CheckWaveClear;
    }

    private void CheckDefeat(float health)
    {
        if(health <= 0 && !_stageData.IsDefeated.Value)
        {
            _stageData.IsDefeated.Value = true;
            _waves[_stageData.CurrentWave.Value].OnExit();
            Debug.Log("스테이지 종료");
        }
    }

    private void CheckWaveClear(bool isClear)
    {
        _waves[_stageData.CurrentWave.Value].OnExit();
        Debug.Log("웨이브 클리어");
        StartCoroutine(WaveCoolRoutine());
        _stageData.Time.Value = _timeForWave;
    }

    private void CacheComponents()
    {
        _stageData = GetComponent<StageData>();
        _waitForCoolDown = new WaitForSeconds(_waveCoolDown);
        _isTimeStopped = false;
    }
}
