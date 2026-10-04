using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusData : MonoBehaviour
{
    private float _currentHealth;
    [SerializeField] private float _maxHealth;
    public float CurrentHealth
    {
        get => _currentHealth;
        set
        {
            _currentHealth = value;
            OnHealthChanged?.Invoke(_currentHealth);
        }
    }

    public float MaxHealth
    {
        get => _maxHealth;
        set
        {
            _maxHealth = value;
            OnMaxHealthChanged?.Invoke(_maxHealth);
        }
    }

    public event Action<float> OnHealthChanged;
    public event Action<float> OnMaxHealthChanged;
}
