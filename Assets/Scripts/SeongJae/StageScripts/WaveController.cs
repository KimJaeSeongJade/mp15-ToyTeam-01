using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    //TODO: 몬스터 오브젝트 풀과 연동
    [SerializeField] private ObjectPool _monsterPool;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _spawnCoolDown = 2.0f;

    public int MaxMonsterCount;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;
    private int _monsterSpawnCount;

    private bool _isDefeated;
    private void Start() => Init();

    public void OnEnter()
    {
        StartSpawnMonster();
        //넥서스 체력 변동 시 호출하도록 구독 한 번?
    }

    public void OnRunning()
    {

    }

    public void OnExit()
    {
        StopSpawnMonster();
    }

    private void CheckGameOver()
    {

    }

    private void SpawnMonsterLine()
    {
        //TODO: 오브젝트 풀, 몬스터 연동, 소환 구현 필요
        //TODO: 소환하는 알고리즘 푸아송 디스크? 사용하면 되나요
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
