using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
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

    [Header("넥서스 객체 연동")]
    public NexusController Nexus;

    public event Action<int> OnCurrentWaveChanged;
    public event Action<int> OnMaxWaveChanged;
    public event Action<int> OnMonsterCountChanged;
    public event Action<int> OnScoreChanged;
    public event Action<int> OnKillCountChanged;
    public event Action<float> OnTimeChanged;
}
