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

    private bool _isFinished;
    private bool _isTimeStopped;

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
        if(time <= 0 && !_isFinished)
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
        _stageData.CurrentWave++;
        if (_stageData.CurrentWave >= _stageData.MaxWave)
        {
            Debug.Log($"{name} : 스테이지 클리어");
            _isTimeStopped = true;
            _isFinished = true;
            return;
        }
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

        _waves[_stageData.CurrentWave].OnEnter();
    }

    private void CacheComponents()
    {
        _stageData = GetComponent<StageData>();
        _waitForCoolDown = new WaitForSeconds(_waveCoolDown);
        _isTimeStopped = false;
    }
}
