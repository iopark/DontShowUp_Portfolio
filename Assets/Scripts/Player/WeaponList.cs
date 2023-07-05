using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Player_WeaponList", menuName = "Registry/Weapon/WeaponList")]
public class WeaponList : ScriptableObject
{
    [SerializeField]
    private RangedList[] rangedList; 
    public RangedList[] RangedLists
    {
        get { return rangedList; }
    }
    [Serializable]
    public class RangedList
    {
        public RangedWeapon weapon; 
    }

    public class MeleeList
    {
        public MeleeWeapon weapon;
    }
}
