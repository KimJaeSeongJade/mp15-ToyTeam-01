using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //TODO: 몬스터 오브젝트 풀 연동
    [SerializeField] private ObjectPool _monsterPool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _maxMonster;
    [SerializeField] private float _spawnCoolDown = 2.0f;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;
    private int _monsterSpawnCount;

    private void Start() => Init();

    /// <summary>
    /// 웨이브 시작 시 실행할 로직
    /// </summary>
    private void OnEnable()
    {
        StartSpawnMonster();
    }

    /// <summary>
    /// 웨이브 종료 시 실행할 로직
    /// </summary>
    private void OnDisable()
    {
        StopSpawnMonster();
    }

    private void SpawnMonsterLine()
    {
        //TODO: 오브젝트 풀, 몬스터 연동, 소환 구현 필요

        Debug.Log($"몬스터 {_monsterSpawnCount}번째 사이클 소환");
        _monsterSpawnCount++;
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return _waitSpawnCoolDown;
            SpawnMonsterLine();
        }
        
    }

    private void StartSpawnMonster()
    {
        if (_monsterSpawnRoutine != null) return;

        _monsterSpawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void StopSpawnMonster()
    {
        if (_monsterSpawnRoutine == null) return;

        StopCoroutine(_monsterSpawnRoutine);
        _monsterSpawnRoutine = null;
    }

    private void Init()
    {
        _waitSpawnCoolDown = new WaitForSeconds(_spawnCoolDown);
    }
}
