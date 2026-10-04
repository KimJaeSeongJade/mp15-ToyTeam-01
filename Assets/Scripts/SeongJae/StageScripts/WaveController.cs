using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField] private ObjectPool _monsterPool;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _spawnCoolDown = 1.0f;
    [SerializeField] private float _monsterAmount = 50;

    public int MaxMonsterCount;
    private int _monsterSpawnCount;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;

    private NexusController _nexusController;
    private NexusData _nexusData;

    public event Action OnWaveCleared;
    public event Action OnWaveDefeated;

    private bool _isDefeated;
    private bool _isRunning;

    private void Start() => Init();
    public void OnEnter()
    {
        _isRunning = true;
        StartSpawnMonster();
        _nexusData.OnHealthChanged += CheckGameOver;
        Debug.Log($"{name} : 웨이브 시작");
    }

    public void OnRunning()
    {

    }

    public void OnExit()
    {
        _isRunning = false;
        StopSpawnMonster();
        OnWaveCleared?.Invoke();
        OnWaveCleared = null;
        _nexusData.OnHealthChanged -= CheckGameOver;
        Debug.Log($"{name} : 웨이브 종료");
    }

    public void SetNexus(NexusController nexusController)
    {
        _nexusController = nexusController;
        _nexusData = _nexusController.GetComponent<NexusData>();
    }

    private void CheckGameOver(float health)
    {
        if(health <= 0 && !_isDefeated)
        {
            Debug.Log($"{name} : 방어물 체력 0 이하 확인");
            _isDefeated = true;
            OnWaveDefeated?.Invoke();
        }
    }

    private void SpawnMonsterLine()
    {
        //TODO: 오브젝트 풀, 몬스터 연동, 소환 구현 필요
        for(int i = 0; i < _monsterAmount; i++)
        {
            IPoolable monster = _monsterPool.Take();
            OnWaveCleared += monster.ReturnToPool;

            monster.GameObject.transform.position = _spawnPoint.position;
            monster.GameObject.GetComponent<MonsterController>().SetNexus(_nexusController);         
        }

        Debug.Log($"{name} : 몬스터 {_monsterSpawnCount}번째 사이클 소환");
        _monsterSpawnCount++;
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isRunning)
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
