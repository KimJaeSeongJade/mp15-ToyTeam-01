using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _monsterRemain;
    [SerializeField] private TextMeshProUGUI _currentWave;
    [SerializeField] private TextMeshProUGUI _maxWave;
    [SerializeField] private TextMeshProUGUI _elapseTime;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private StageData _stageData;
    
    public string MonsterRemain => _monsterRemain.text;
    public string CurrentWave => _currentWave.text;
    public string MaxWave => _maxWave.text;
    public string ElapseTime => _elapseTime.text;
    public string Score => _scoreText.text;
    
    private void OnEnable() => BindGameFlow();
    private void OnDisable() => UnbindGameFlow();

    private void BindGameFlow()
    {
        _stageData.OnMonsterCountChanged += CurrentMonsterRemainText;
        _stageData.OnMaxWaveChanged += MaxWaveText;
        _stageData.OnCurrentWaveChanged += CurrentWaveText;
        _stageData.OnTimeChanged += TimeChanged;
        _stageData.OnScoreChanged += CurrentScoreText;
    }

    private void UnbindGameFlow()
    {
        _stageData.OnMonsterCountChanged -= CurrentMonsterRemainText;
        _stageData.OnMaxWaveChanged -= MaxWaveText;
        _stageData.OnCurrentWaveChanged -= CurrentWaveText;
        _stageData.OnTimeChanged -= TimeChanged;
        _stageData.OnScoreChanged -= CurrentScoreText;
    }

    private void CurrentMonsterRemainText(int monsterRemain)
    {
        _monsterRemain.text = $"{monsterRemain}";
    }

    private void MaxWaveText(int maxWave)
    {
        _maxWave.text = $"{maxWave}";
    }

    private void CurrentWaveText(int currentWave)
    {
        _currentWave.text = $"{currentWave}";
        
    }

    private void TimeChanged(float time)
    {
        _elapseTime.text = $"{time}";
    }

    private void CurrentScoreText(int currentScore)
    {
        _scoreText.text = $"{currentScore}";
    }
}
