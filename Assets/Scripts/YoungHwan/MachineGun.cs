using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : TurretBase
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _range;
    public override void Attack()
    {
        Ray ray = new Ray(_firePoint.position, _firePoint.forward);
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
