using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    // ========= 데이터 =========

    [Header("넥서스 객체 연동")]
    public NexusController Nexus;

    // 현재 웨이브 수
    private int _currentWave;
    public int CurrentWave
    {
        get => _currentWave;
        set
        {
            _currentWave = value;
            OnCurrentWaveChanged?.Invoke(_currentWave);
        }
    }

    // 최대 웨이브 수
    private int _maxWave;
    public int MaxWave
    {
        get => _maxWave;
        set
        {
            _maxWave = value;
            OnMaxWaveChanged?.Invoke(_maxWave);
        }
    }

    // 현재 몬스터 수
    private int _monsterCount;
    public int MonsterCount
    {
        get => _monsterCount;
        set
        {
            _monsterCount = value;
            OnMonsterCountChanged?.Invoke(_monsterCount);
        }
    }

    // 현재 점수
    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreChanged?.Invoke(_score);
        }
    }

    // 처치한 몬스터 수
    private int _killCount;
    public int KillCount
    {
        get => _killCount;
        set
        {
            _killCount = value;
            OnKillCountChanged?.Invoke(_killCount);
        }
    }

    [Header("웨이브 경과 시간")]
    [SerializeField] private float _time;
    public float Time
    {
        get => _time;
        set
        {
            _time = value;
            OnTimeChanged?.Invoke(_time);
        }
    }
    
    // 스테이지 클리어 결과
    private bool _isClear = false;
    public bool IsClear
    {
        get => _isClear;
        set
        {
            _isClear = value;
            OnStageCleared?.Invoke();
            OnStageCleared = null;
        }
    }

    // ========= 초기화 ========= 
    public void SetData(int maxWave, int monsterCount, float time)
    {
        _currentWave = 0;
        _maxWave = maxWave;
        _monsterCount = monsterCount;
        _killCount = 0;
        _score = 0;
        _time = time;
    }

    // ========= 스코어 관련 이벤트 ========= 

    // 몬스터 웨이브 변동 이벤트
    public event Action<int> OnCurrentWaveChanged;
    public event Action<int> OnMaxWaveChanged;
    
    // 현재 몬스터 수 변동 이벤트
    public event Action<int> OnMonsterCountChanged;
    
    // 점수 변동 이벤트
    public event Action<int> OnScoreChanged;

    // 몬스터 처치 수 변동 이벤트
    public event Action<int> OnKillCountChanged;
    
    // 웨이브 타이머 변동 이벤트
    public event Action<float> OnTimeChanged;

    // ========= 스테이지 클리어 관련 이벤트 ========= 

    // 스테이지 클리어 여부 변동 이벤트
    public event Action OnStageCleared;
}
