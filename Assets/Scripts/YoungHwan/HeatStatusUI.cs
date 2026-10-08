using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        

        if (_heatCurrent.fillAmount < 0.75f)
        {
            _heatCurrent.color = new Color32(255, 175, 83, 255);
        }
        
        if (_heatCurrent.fillAmount > 0.75f)
        {
            _heatCurrent.color = new Color32(255, 106, 0, 255);
        }

        bool _isPopMaxHeat = turret.OverHeat;
        
        if (!_isPopMaxHeat)
        {
            _heatText.color = new Color32(252, 135, 83, 255);
            _heatText.text = "HEAT";
        }
        
        if (_isPopMaxHeat)
        {
            _heatText.color = new Color32(255, 56, 0, 255);
            _heatText.text = "OVERHEAT";
        }
    }
}
