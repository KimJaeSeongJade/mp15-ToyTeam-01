using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class NexusUIController : MonoBehaviour
{
    [SerializeField] private Image _gauge;
    [SerializeField] private TextMeshProUGUI _text;

    private float _maxHealth;

    // 1. 현재 넥서스 체력 표기 (이미지)
    public void RefreshHealthBar(float currentHealth)
    {

    }
    // 2. 현재 체력 / 전체 체력 (텍스트)
    public void RefreshCurrentHealthText(float currentHealth)
    {
        _text.text = $"{currentHealth} / {_maxHealth}";
    }

    public void RefreshMaxHealthText(float maxHealth)
    {
        _maxHealth = maxHealth;
    }
}
