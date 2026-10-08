using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageData : MonoBehaviour
{
    [Header("넥서스 객체 연동")]
    public NexusData NexusData;

    public ObserveableProperty<int> CurrentWave = new();
    public ObserveableProperty<int> MaxWave = new();
    public ObserveableProperty<int> MonsterCount = new();
    public ObserveableProperty<int> Score = new();
    public ObserveableProperty<int> KillCount = new();
    public ObserveableProperty<bool> IsStageClear = new();
    public ObserveableProperty<bool> IsDefeated = new();
    public ObserveableProperty<float> Time = new();
    public void SetData(int maxWave, int monsterCount, float time)
    {
        CurrentWave.Value = 0;
        KillCount.Value = 0;
        Score.Value = 0;

        MonsterCount.Value = monsterCount;
        MaxWave.Value = maxWave;        
        Time.Value = time;
    }
}
