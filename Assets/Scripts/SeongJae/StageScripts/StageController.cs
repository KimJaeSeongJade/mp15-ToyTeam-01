using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [Header("사용할 웨이브 목록")]
    [SerializeField] private List<WaveController> _waves = new();

    [Header("웨이브 실행 주기")]
    [SerializeField] private float _waveCoolDown = 5.0f;

    private WaitForSeconds _waitForFinishWave;
    private StageData _stageData;

    private void Awake() => CacheComponents();
    private void Start() => Init();

    private void Update()
    {
        _stageData.CurrentTime -= Time.deltaTime;
        Debug.Log("경과 시간 : " + _stageData.CurrentTime);
    }

    private IEnumerator WaveRoutine()
    {
        yield return _waitForFinishWave;
    }

    private void Init()
    {
        _stageData.CurrentWave = 0;
        _stageData.MaxWave = _waves.Count;
    }

    private void CacheComponents()
    {
        _stageData = GetComponent<StageData>();
        _waitForFinishWave = new WaitForSeconds(_waveCoolDown);
    }
}
