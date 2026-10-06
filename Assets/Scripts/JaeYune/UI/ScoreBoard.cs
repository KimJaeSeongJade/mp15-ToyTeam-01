using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _monsterRemainText;
    [SerializeField] private TextMeshProUGUI _currentWaveText;
    [SerializeField] private TextMeshProUGUI _maxWaveText;
    [SerializeField] private TextMeshProUGUI _elapseTimeText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    //[SerializeField] private InGameUIController _inGameUIController;

}
