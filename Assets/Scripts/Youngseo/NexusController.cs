using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusController : MonoBehaviour, IDamageable
{
    NexusData _nexusData;

    private void Start() => Init();

    private void Init()
    {
        _nexusData.CurrentHealth = _nexusData.MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        if(damage >= _nexusData.CurrentHealth)
        {
            _nexusData.CurrentHealth = 0;
        }
        else
        {
            _nexusData.CurrentHealth -= damage;
        }
    }
}
