using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonWeapon : Weapon
{
    public GameObject projectile;
    public Transform throwTF;

    public override void SpecialAttack()
    {
        base.SpecialAttack();
        Instantiate(projectile, throwTF.position, transform.rotation);
        WeaponBroke();
    }
}
