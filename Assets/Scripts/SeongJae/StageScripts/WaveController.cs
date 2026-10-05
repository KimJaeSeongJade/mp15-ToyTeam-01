using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveController : MonoBehaviour
{
    private WaveData _waveData;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;

    private NexusController _nexusController;
    private NexusData _nexusData;

    private StageData _stageData;
    private bool _isRunning;

    private Vector3 _tempSpawnPoint;
    private Vector2 _randomSpawnPoint;

    [SerializeField] private float minOffset, maxOffset;

    private event Action<float> KillTestEvent;
    // =============== 유니티 생명 주기 =============== 

    private void Start() => Init();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) KillTestEvent?.Invoke(1000);
    }
    // =============== 웨이브 진행 시점에서 수행할 행동 ===============

    public void OnEnter()
    {
        _isRunning = true;
        StartSpawn();
        _nexusData.OnHealthChanged += CheckGameOver;
        Debug.Log($"{name} : 웨이브 시작");
    }

    public void OnExit()
    {
        _isRunning = false;
        StopSpawn();
        //_waveData.IsCleared = true;
        _nexusData.OnHealthChanged -= CheckGameOver;
        Debug.Log($"{name} : 웨이브 종료");
    }
    
    public void Clear()
    {
        if (_waveData.IsCleared) return;
        _waveData.IsCleared = true;
    }
    // =============== 부모에서 넥서스 정보를 반환 ===============

    public void SetData(StageData stageData)
    {
        _stageData = stageData;
        _nexusController = stageData.Nexus;
        _nexusData = _nexusController.GetComponent<NexusData>();
    }
    // =============== 패배 조건 확인 ===============
    private void CheckGameOver(float health)
    {
        if(health <= 0 && !_waveData.IsDefeated)
        {
            _waveData.IsDefeated = true;
        }
    }
    // =============== 한 몬스터 소환 주기 ===============

    private void SetSpawnPoint(Transform tr)
    {
        _randomSpawnPoint.x = UnityEngine.Random.Range(minOffset, maxOffset);
        _randomSpawnPoint.y = UnityEngine.Random.Range(minOffset, maxOffset);
        _tempSpawnPoint = new Vector3(_waveData.SpawnPoint.position.x + _randomSpawnPoint.x, _tempSpawnPoint.y, _waveData.SpawnPoint.position.z + _randomSpawnPoint.y);
        tr.position = _tempSpawnPoint;
    }

    private void SpawnLine()
    {
        for(int i = 0; i < _waveData.SpawnAmount; i++)
        {
            IPoolable monster = _waveData.MonsterPool.Take();
            _waveData.OnWaveCleared += monster.ReturnToPool;
            _waveData.OnDefeated += monster.ReturnToPool;

            SetSpawnPoint(monster.GameObject.transform);
            monster.GameObject.GetComponent<MonsterController>().SetNexus(_nexusController);
            monster.GameObject.GetComponent<MonsterController>().OnKilled += RefreshScore;
            KillTestEvent += (monster as IDamageable).TakeDamage;
        }
    }
    // =============== 몬스터 소환 코루틴 ===============

    private IEnumerator SpawnRoutine()
    {
        while (_isRunning)
        {
            yield return _waitSpawnCoolDown;
            SpawnLine();
        }
        
    }
    // =============== 몬스터 소환 코루틴 실행 ===============

    private void StartSpawn()
    {
        if (_monsterSpawnRoutine != null) return;

        _monsterSpawnRoutine = StartCoroutine(SpawnRoutine());
    }
    // =============== 몬스터 소환 코루틴 정지 ===============

    private void StopSpawn()
    {
        if (_monsterSpawnRoutine == null) return;

        StopCoroutine(_monsterSpawnRoutine);
        _monsterSpawnRoutine = null;
    }
    // =============== 몬스터 사망 이벤트에 구독할 메서드 ===============
    private void RefreshScore(MonsterData monsterData)
    {
        _stageData.KillCount++;
        _stageData.Score += monsterData.Score;
        _waveData.AmountForClear--;
        if(_waveData.AmountForClear <= 0)
        {
            _waveData.IsCleared = true;
        }
    }
    // =============== 정보 초기화 메서드 ===============

    private void Init()
    {
        _waveData = GetComponent<WaveData>();
        _waitSpawnCoolDown = new WaitForSeconds(_waveData.SpawnCoolDown);
        _tempSpawnPoint = _waveData.SpawnPoint.position;
    }
}