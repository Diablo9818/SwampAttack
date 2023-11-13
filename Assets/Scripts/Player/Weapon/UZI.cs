using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI : Weapon
{
    public override void Shoot()
    {
        Instantiate(Bullet, _shootPoint.position, Quaternion.identity);
    }
}
