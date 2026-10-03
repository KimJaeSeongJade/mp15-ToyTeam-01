using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusInfo : MonoBehaviour
{
    [SerializeField] private float _nexusHealth = 10000f;
    public float CurrentHealth;

    public void Init()
    {
        CurrentHealth = _nexusHealth;
    }

    private void takeDamage(float damage)
    {
        CurrentHealth -= damage;

        if(CurrentHealth < 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
