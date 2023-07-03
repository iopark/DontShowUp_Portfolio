using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,
    Ranged
}
public class WeaponSO : ScriptableObject
{
    public WeaponType type; 
    public string weaponName;
    public GameObject weaponPrefab; 
    public int damage;
    public int weaponRange; 
    public float attackRate;
    public float noiseIntensity; 
}
