using UnityEngine;

public class MachineGun : TurretBase
{
    [Header("머신건 - 레이어")]
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _monsterLayer;
    [Header("머신건 - 이펙트")]
    [SerializeField] private GameObject _hitEffect;
    [Header("머신건 - 발사")]
    [SerializeField] private int _minBullets;
    [SerializeField] private int _maxBullets;
    [SerializeField] private float _spreadAngle = 1f;
    [Header("머신건 - 판정")]
    [SerializeField] private float _bulletRange;
    [SerializeField] private float _bulletRadius = 0.1f;
    [SerializeField] private float _explosionRadius = 0.3f;
    [SerializeField] private float _bulletDamage = 5f;
    public override void Attack()
    {
        int bullets = Random.Range(_minBullets, _maxBullets + 1);
        for (int i = 0; i < bullets; i++)
        {
            Quaternion spread = Quaternion.Euler(Random.Range(-_spreadAngle, _spreadAngle), Random.Range(-_spreadAngle, _spreadAngle), 0f);
            Vector3 direction = _firePoint.rotation * spread * Vector3.forward;
            Ray ray = new Ray(_firePoint.position, direction);
            RaycastHit hit;
            if (Physics.SphereCast(ray, _bulletRadius, out hit,  _bulletRange, _targetLayer))
            {
                GameObject hitEffect = Instantiate(_hitEffect);
                hitEffect.transform.position = hit.point;
                Destroy(hitEffect, 1f);
                
                Collider[] monsters = Physics.OverlapSphere(hit.point, _explosionRadius, _monsterLayer);
                foreach (Collider monster in monsters)
                {
                    monster.GetComponent<IDamageable>().TakeDamage(_bulletDamage);
                }
            }
        }
    }
}
