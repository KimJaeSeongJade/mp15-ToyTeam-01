using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : TurretBase
{
    public override void Attack()
    {
        // TODO: 머신건 공격 로직 구현
        SpawnBullet();
    }
}
