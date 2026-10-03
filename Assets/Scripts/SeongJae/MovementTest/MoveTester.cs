using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTester : MonoBehaviour, IDamageable
{
    [SerializeField] private float _moveSpeed = 10.0f;
    public void TakeDamage(float damage)
    {

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
    }
}
