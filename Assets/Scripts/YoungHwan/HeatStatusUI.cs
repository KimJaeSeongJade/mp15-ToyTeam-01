using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeatGaugeUI : MonoBehaviour
{
    [SerializeField] private TurretController _controller;
    [SerializeField] private Image _heatCurrent;
    [SerializeField] private TextMeshProUGUI _heatText;

    private TurretBase _turret => _controller.CurrentTurret;
    private bool _isPopMaxHeat => _turret.OverHeat;
    
    private void Update()
    {
        if (_turret == null) 
            return;

        _heatCurrent.fillAmount = _turret.HeatGauge;
        
        GaugeColorChange();
        TextColorChange();
    }
    
    private void GaugeColorChange()
    {
        if (_heatCurrent.fillAmount < 0.75f)
        {
            _heatCurrent.color = new Color32(255, 175, 83, 255);
        }
        
        if (_heatCurrent.fillAmount > 0.75f)
        {
            _heatCurrent.color = new Color32(255, 106, 0, 255);
        }
    }
    
    private void TextColorChange()
    {
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
