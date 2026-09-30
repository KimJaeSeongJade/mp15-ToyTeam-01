using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [Header("포탑 설정")]
    [SerializeField] protected TurretType _type;
    [SerializeField] protected Transform _firePoint;
    
    public TurretType Type => _type;

    protected abstract void Fire();
}
