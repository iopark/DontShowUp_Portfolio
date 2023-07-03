using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [Header("Player Stat")]
    private float moveSpeed;
    private float runSpeed; 
    private int maxHealth; 
    private int health;
    public event UnityAction<int> OnHealthChange;

    [Header("Weapon Stat")]
    private int damage;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private float fireRate;
    public float FireRate
    {
        get { return fireRate; }
        set { fireRate = value; }
    }
    private float reloadSpeed;
    public float ReloadSpeed
    {
        get { return reloadSpeed; }
        set { reloadSpeed = value; }
    }
    private float noiseIntensity; 
    public float NoiseIntensity
    {
        get { return noiseIntensity; }
        set { noiseIntensity = value; }
    }

}
