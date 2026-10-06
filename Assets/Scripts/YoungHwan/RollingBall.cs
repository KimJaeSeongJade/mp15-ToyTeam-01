using UnityEngine;

public class RollingBall : TurretBase
{
    [Header("롤링볼")]
    [SerializeField] private Transform _secondFirePoint;
    [SerializeField] private float _spreadAngle; 
    public override void Attack()
    {
        Vector3 direction = _firePoint.forward;
        direction.y = 0;
        if (direction == Vector3.zero) return;
        
        Quaternion rotation = Quaternion.LookRotation(direction.normalized);
        
        Quaternion leftRotation = rotation * Quaternion.Euler(0, -_spreadAngle, 0);
        Quaternion rightRotation = rotation * Quaternion.Euler(0, _spreadAngle, 0);

        SpawnBullet(_firePoint.position, leftRotation);
        SpawnBullet(_secondFirePoint.position, rightRotation);
    }
    public override void SetFirePoint(Transform model)
    {
        base.SetFirePoint(model);

        Transform second = model.Find("SecondFirePoint");
        if (second != null) _secondFirePoint = second;
    }
}
