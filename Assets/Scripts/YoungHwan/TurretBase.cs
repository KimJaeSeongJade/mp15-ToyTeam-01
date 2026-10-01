using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretBase : MonoBehaviour
{
    [SerializeField] protected TurretType _type;
    [SerializeField] protected Transform _firePoint;
    
    public TurretType Type => _type;

    public abstract void Fire();
}
