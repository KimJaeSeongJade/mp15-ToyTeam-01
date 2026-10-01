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

    private int _currentMonster;
    public int CurrentMonster
    {
        get => _currentMonster;
        set
        {
            _currentMonster = value;
            OnMonsterCountChanged?.Invoke(_currentMonster);
        }
    }

    private int _currentScore;
    public int CurrentScore
    {
        get => _currentScore;
        set
        {
            _currentScore = value;
            OnScoreChanged?.Invoke(_currentScore);
        }
    }

    public event Action<int> OnCurrentWaveChanged;
    public event Action<int> OnMaxWaveChanged;
    public event Action<int> OnMonsterCountChanged;
    public event Action<int> OnScoreChanged;
}
