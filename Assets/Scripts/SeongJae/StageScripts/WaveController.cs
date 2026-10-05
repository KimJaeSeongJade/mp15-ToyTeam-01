using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("현재 사용할 몬스터 오브젝트 풀 참조")]
    [SerializeField] private ObjectPool _monsterPool;

    [Header("몬스터 소환 지점")]
    [SerializeField] private Transform _spawnPoint;

    [Header("몬스터 소환 주기")]
    [SerializeField] private float _spawnCoolDown = 1.0f;

    [Header("한 소환 주기에서 소환할 몬스터 수")]
    [SerializeField] private float _monsterAmount = 50;

    [Header("최대 몬스터 소환 수")]
    public int MaxMonsterCount;


    private int _monsterSpawnCount;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;

    private NexusController _nexusController;
    private NexusData _nexusData;

    private StageData _stageData;

    public event Action OnWaveCleared;
    public event Action OnWaveDefeated;

    private bool _isDefeated;
    private bool _isRunning;

    //public event Action<float> KillTest;

    // =============== 유니티 생명 주기 =============== 

    private void Start() => Init();
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        KillTest?.Invoke(1000);
    //        KillTest = null;
    //    }
    //}

    // =============== 웨이브 진행 시점에서 수행할 행동 ===============

    public void OnEnter()
    {
        _isRunning = true;
        StartSpawnMonster();
        _nexusData.OnHealthChanged += CheckGameOver;
        Debug.Log($"{name} : 웨이브 시작");
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

    // =============== 부모에서 넥서스 정보를 반환 ===============

    public void SetNexus(NexusController nexusController)
    {
        _nexusController = nexusController;
        _nexusData = _nexusController.GetComponent<NexusData>();
    }

    public void SetStageData(StageData stageData)
    {
        _stageData = stageData;
    }

    // =============== 패배 조건 확인 ===============
    private void CheckGameOver(float health)
    {
        if(health <= 0 && !_isDefeated)
        {
            Debug.Log($"{name} : 방어물 체력 0 이하 확인");
            _isDefeated = true;
            OnWaveDefeated?.Invoke();
        }
    }

    // =============== 한 몬스터 소환 주기 ===============

    private void SpawnMonsterLine()
    {
        for(int i = 0; i < _monsterAmount; i++)
        {
            IPoolable monster = _monsterPool.Take();
            OnWaveCleared += monster.ReturnToPool;

            monster.GameObject.transform.position = _spawnPoint.position;
            monster.GameObject.GetComponent<MonsterController>().SetNexus(_nexusController);
            monster.GameObject.GetComponent<MonsterController>().OnKilled += IncreaseScore;
            //KillTest += (monster as IDamageable).TakeDamage;
        }

        Debug.Log($"{name} : 몬스터 {_monsterSpawnCount}번째 사이클 소환");
        _monsterSpawnCount++;
    }

    // =============== 몬스터 소환 코루틴 ===============

    private IEnumerator SpawnRoutine()
    {
        while (_isRunning)
        {
            yield return _waitSpawnCoolDown;
            SpawnMonsterLine();
        }
        
    }

    // =============== 몬스터 소환 코루틴 실행 ===============

    private void StartSpawnMonster()
    {
        if (_monsterSpawnRoutine != null) return;

        _monsterSpawnRoutine = StartCoroutine(SpawnRoutine());
    }

    // =============== 몬스터 소환 코루틴 정지 ===============

    private void StopSpawnMonster()
    {
        if (_monsterSpawnRoutine == null) return;

        StopCoroutine(_monsterSpawnRoutine);
        _monsterSpawnRoutine = null;
    }

    // =============== 몬스터 사망 이벤트에 구독할 메서드 ===============
    private void IncreaseScore(MonsterData monsterData)
    {
        _stageData.KillCount++;
        _stageData.Score += monsterData.Score;
        Debug.Log($"{monsterData.Type} 몬스터 사망, 점수 {monsterData.Score} 상승 {_stageData.Score}");
        Debug.Log($"현재 처치한 몬스터 수 : {_stageData.KillCount}");
    }


    // =============== 정보 초기화 메서드 ===============

    private void Init()
    {
        _waitSpawnCoolDown = new WaitForSeconds(_spawnCoolDown);
    }
}
