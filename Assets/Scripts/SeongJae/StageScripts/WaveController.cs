using System;
using System.Collections;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    private WaveData _waveData;

    private WaitForSeconds _waitSpawnCoolDown;
    private Coroutine _monsterSpawnRoutine;

    private StageData _stageData;
    private bool _isRunning;

    private Vector3 _tempSpawnPoint;
    [SerializeField] private float minOffset, maxOffset;
    private event Action OnClearMonster;
    private event Action<float> TestKill;
    private void Awake() => CacheComponents();
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
    }

    public void OnExit()
    {
        _isRunning = false;
        StopSpawn();
        _stageData.Time.OnValueChanged -= CheckTimeOver;
        OnClearMonster?.Invoke();
        OnClearMonster = null;
    }

    public void SetData(StageData stageData)
    {
        _stageData = stageData;
    }

    private void SetSpawnPoint(Transform tr, int index)
    {
        float randomX = UnityEngine.Random.Range(minOffset, maxOffset);
        float randomZ = UnityEngine.Random.Range(minOffset, maxOffset);

        Transform targetPoint = _waveData.SpawnPoint[index];

        _tempSpawnPoint = targetPoint.position + (targetPoint.right * randomX) + (targetPoint.forward * randomZ);

        tr.position = _tempSpawnPoint;
        tr.rotation = targetPoint.rotation;
    }

    private void SpawnLine()
    {
        for(int j  = 0; j < _waveData.SpawnPoint.Count; j++)
        {
            for (int i = 0; i < _waveData.SpawnAmount; i++)
            {
                IPoolable monster = _waveData.Source.MonsterPool[_waveData.Type].Take();
                OnClearMonster += monster.ReturnToPool;
                TestKill += (monster as IDamageable).TakeDamage;

                SetSpawnPoint(monster.GameObject.transform, j);
                monster.GameObject.GetComponent<MonsterController>().OnKilled += RefreshScore;
            }
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
        _waitSpawnCoolDown = new WaitForSeconds(_waveData.SpawnCoolDown);
    }
    
    private void CacheComponents()
    {
        _waveData = GetComponent<WaveData>();
    }
}