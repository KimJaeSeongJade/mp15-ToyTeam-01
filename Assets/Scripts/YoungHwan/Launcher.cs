using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : TurretBase
{
    public override void Attack()
    {
        SpawnBullet(_firePoint.position, _firePoint.rotation);
    }
}
