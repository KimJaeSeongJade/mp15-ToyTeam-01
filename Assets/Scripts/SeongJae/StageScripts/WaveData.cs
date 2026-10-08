using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveData : MonoBehaviour
{
    [Header("소환할 몬스터가 담긴 오브젝트 풀")]
    public ObjectPool SmallMonsterPool;
    public ObjectPool MiddleMonsterPool;
    public ObjectPool BigMonsterPool;

    public Dictionary<MonsterType, ObjectPool> MonsterPool = new();

    [Header("몬스터 생성이 발생하는 중심점")]
    public Transform SpawnPoint;

    [Header("소환 주기")]
    public float SpawnCoolDown;
    
    [Header("한 주기에서 소환하는 몬스터 수")]
    public int SpawnAmount;
    
    [Header("처치 해야 하는 몬스터 수")]
    public int AmountForClear;

    public ObserveableProperty<bool> IsWaveClear = new();

    private void Start() => Init();

    private void Init()
    {
        MonsterPool.Add(MonsterType.SMALL, SmallMonsterPool);
        MonsterPool.Add(MonsterType.MIDDLE, MiddleMonsterPool);
        MonsterPool.Add(MonsterType.BIG, BigMonsterPool);
    }
}
