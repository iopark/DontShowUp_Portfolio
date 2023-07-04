using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ranged_", menuName ="Registry/Weapon/Ranged")]
public class RangedWeapon : WeaponSO
{
    public Launcher launcher; 
    public float reloadRate;
    public int roundLimit;
}
