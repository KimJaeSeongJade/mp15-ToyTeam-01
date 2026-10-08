using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    [Header("소환할 몬스터가 담긴 오브젝트 풀")]
    public MonsterPoolController Source;

    [Header("몬스터 생성이 발생하는 중심점")]
    public Transform SpawnPoint;

    [Header("소환 주기")]
    public float SpawnCoolDown;
    
    [Header("한 주기에서 소환하는 몬스터 수")]
    public int SpawnAmount;
    
    [Header("처치 해야 하는 몬스터 수")]
    public int AmountForClear;

    [Header("소환 할 몬스터 종류")]
    public MonsterType Type;

    public ObserveableProperty<bool> IsWaveClear = new();
}
