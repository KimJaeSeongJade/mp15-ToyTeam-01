using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeatGaugeUI : MonoBehaviour
{
    [SerializeField] private TurretController _controller;
    [SerializeField] private Image _heatCurrent;
    [SerializeField] private TextMeshProUGUI _heatText;

    private void Update()
    {
        TurretBase turret = _controller.CurrentTurret;
        if (turret == null) return;

        _heatCurrent.fillAmount = turret.HeatGauge;
    }
}
