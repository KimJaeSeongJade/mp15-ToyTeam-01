using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : TurretBase
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private int _minBullets;
    [SerializeField] private int _maxBullets;
    [SerializeField] private float _range;
    [SerializeField] private float _spreadAngle = 2.5f;
    public override void Attack()
    {
        int bullets = Random.Range(_minBullets, (_maxBullets + 1));
        for (int i = 0; i < bullets; i++)
        {
            Quaternion spread = Quaternion.Euler(
                Random.Range(-_spreadAngle, _spreadAngle),
                Random.Range(-_spreadAngle, _spreadAngle), 0f);
            Vector3 direction = _firePoint.rotation * spread * Vector3.forward;
            Ray ray = new Ray(_firePoint.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,  _range, _targetLayer))
            {
                Debug.Log($"hit {hit.collider.gameObject.name}");
                Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.red, 0.1f);
            }
            else
            {
                // 테스트하는 레이 입니다
                Debug.DrawRay(ray.origin, ray.direction * _range, Color.yellow, 0.1f);
            }
        }
    }
    
}
