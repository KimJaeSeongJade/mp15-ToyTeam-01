using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveData _waveData;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;

    private StageData _stageData;
    private bool _isRunning;

    private Vector3 _tempSpawnPoint;
    private Vector2 _randomSpawnPoint;

    [SerializeField] private float minOffset, maxOffset;
    private event Action OnClearMonster;
    private event Action<float> TestKill;

    private void Start() => Init();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) TestKill?.Invoke(1000);
    }

    public void OnEnter()
    {
        _isRunning = true;
        StartSpawn();
        _stageData.MonsterCount.Value = _waveData.AmountForClear;
        _stageData.Time.OnValueChanged += CheckTimeOver;
        Debug.Log($"{name} : 웨이브 시작");
    }

    public void OnExit()
    {
        _isRunning = false;
        StopSpawn();
        _stageData.Time.OnValueChanged -= CheckTimeOver;
        OnClearMonster?.Invoke();
        OnClearMonster = null;
        Debug.Log($"{name} : 웨이브 종료");
    }

    public void SetData(StageData stageData)
    {
        _stageData = stageData;
    }

    private void SetSpawnPoint(Transform tr)
    {
        _randomSpawnPoint.x = UnityEngine.Random.Range(minOffset, maxOffset);
        _randomSpawnPoint.y = UnityEngine.Random.Range(minOffset, maxOffset);
        _tempSpawnPoint = new Vector3(_waveData.SpawnPoint.position.x, _tempSpawnPoint.y, _waveData.SpawnPoint.position.z + _randomSpawnPoint.y);
        tr.position = _tempSpawnPoint;
        tr.rotation = _waveData.SpawnPoint.rotation;
    }

    private void SpawnLine()
    {
        for(int i = 0; i < _waveData.SpawnAmount; i++)
        {
            IPoolable monster = _waveData.Source.MonsterPool[_waveData.Type].Take();
            OnClearMonster += monster.ReturnToPool;
            TestKill += (monster as IDamageable).TakeDamage;

            SetSpawnPoint(monster.GameObject.transform);
            monster.GameObject.GetComponent<MonsterController>().OnKilled += RefreshScore;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (_isRunning)
        {
            yield return _waitSpawnCoolDown;
            SpawnLine();
        }
    }

    private void StartSpawn()
    {
        if (_monsterSpawnRoutine != null) return;

        _monsterSpawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void StopSpawn()
    {
        if (_monsterSpawnRoutine == null) return;

        StopCoroutine(_monsterSpawnRoutine);
        _monsterSpawnRoutine = null;
    }

    private void RefreshScore(MonsterData monsterData)
    {
        if (_stageData.MonsterCount.Value <= 0) return;

        _stageData.KillCount.Value++;
        _stageData.MonsterCount.Value--;
        _stageData.Score.Value += monsterData.Score;

        if (_stageData.MonsterCount.Value == 0)
        {
            _waveData.IsWaveClear.Value = true;
            return;
        }
    }

    private void CheckTimeOver(float time)
    {
        if(time <= 0 && !_waveData.IsWaveClear.Value)
        {
            _waveData.IsWaveClear.Value = true;
        }
    }

    private void Init()
    {
        _waveData = GetComponent<WaveData>();
        _waitSpawnCoolDown = new WaitForSeconds(_waveData.SpawnCoolDown);
        _tempSpawnPoint = _waveData.SpawnPoint.position;
    }
}