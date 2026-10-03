using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusDamageInfo : MonoBehaviour
{
    [SerializeField] private float _monsterDamage = 10f;

    private float CurrentHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            CurrentHealth -= _monsterDamage;
        }
    }
}
