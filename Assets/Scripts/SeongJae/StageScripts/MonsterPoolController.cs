using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPoolController : MonoBehaviour
{
    [Header("소형 몬스터 오브젝트 풀")]
    public ObjectPool SmallPool;

    [Header("중형 몬스터 오브젝트 풀")]
    public ObjectPool MiddlePool;

    [Header("대형 몬스터 오브젝트 풀")]
    public ObjectPool BigPool;

    public Dictionary<MonsterType, ObjectPool> MonsterPool = new();

    private void Start() => Init();

    private void Init()
    {
        MonsterPool.Add(MonsterType.SMALL, SmallPool);
        MonsterPool.Add(MonsterType.MIDDLE, MiddlePool);
        MonsterPool.Add(MonsterType.BIG, BigPool);
    }

}
