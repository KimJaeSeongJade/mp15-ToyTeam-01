using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ResultBoard : MonoBehaviour
{
    [Header("결과값 텍스트 설정부")]
    [SerializeField] private TextMeshProUGUI _monsterRemain;
    [SerializeField] private TextMeshProUGUI _currentWave;
    [SerializeField] private TextMeshProUGUI _maxWave;
    [SerializeField] private TextMeshProUGUI _elapseTime;
    [SerializeField] private TextMeshProUGUI _score;

    public void ShowResult(StageData dataResult)
    {
        _monsterRemain.text = $"{dataResult.MonsterCount}";
        _currentWave.text = $"{dataResult.CurrentWave + 1}";
        _maxWave.text = $"{dataResult.MaxWave}";
        _elapseTime.text = $"{Mathf.Round(dataResult.Time)}";
        _score.text = $"{dataResult.Score}";
    }
}
