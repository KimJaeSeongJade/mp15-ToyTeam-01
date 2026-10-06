using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _monsterRemainText;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _maxWaveText;
    [SerializeField] private TextMeshProUGUI _elapseTimeText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [SerializeField] private StageData _stageData;
    
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
        _monsterRemainText.text = $"{monsterRemain}";
    }

    private void MaxWaveText(int maxWave)
    {
        _maxWaveText.text = $"{maxWave}";
    }

    private void CurrentWaveText(int currentWave)
    {
        _currentWaveText.text = $"{currentWave}";
    }

    private void TimeChanged(float time)
    {
        _elapseTimeText.text = $"{time}";
    }

    private void CurrentScoreText(int currentScore)
    {
        _scoreText.text = $"{currentScore}";
    }
}
