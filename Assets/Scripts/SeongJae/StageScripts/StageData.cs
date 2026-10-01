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
            OnWaveChanged?.Invoke(_currentWave);
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

    public event Action<int> OnWaveChanged;
    public event Action<int> OnMonsterCountChanged;
}
