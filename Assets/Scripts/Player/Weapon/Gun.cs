using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, _shootPoint.position, Quaternion.identity);
    }
}
