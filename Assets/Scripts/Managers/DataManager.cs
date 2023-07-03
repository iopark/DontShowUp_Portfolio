using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [Header("Player Stat")]
    private float moveSpeed;
    private float crouchSpeed;
    private float runSpeed;
    private float mouseSensitivity; 
    private int maxHealth; 
    private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health -= value;
            OnHealthChange?.Invoke(health); 
            if (health <= 0)
            {
                OnPlayerDeath?.Invoke(); 
            }
        }
    }
    public event UnityAction<int> OnHealthChange;
    public event UnityAction OnPlayerDeath; //TODO: Link up the sound, UI for the player death. 

    [Header("WeaponStat")]
    private int currentWeaponIndex; 
    public int CurrentWeaponIndex
    {
        get { return currentWeaponIndex; }
        set { currentWeaponIndex = value; }
    }

    public WeaponSO primaryWeapon; 
    public WeaponSO secondaryWeapon;

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
    private float bulletMoveSpeed; 
    public float BulletMoveSpeed
    {
        get { return bulletMoveSpeed; }
        set { bulletMoveSpeed = value; }
    }
    private float reloadSpeed;
    public float ReloadSpeed
    {
        get { return reloadSpeed; }
        set { reloadSpeed = value; }
    }
    private float gunNoiseIntensity; 
    public float GunNoiseIntensity
    {
        get { return gunNoiseIntensity; }
        set { gunNoiseIntensity = value; }
    }

    [Header("Weapon Particles")]
    public WaitForSeconds bulletResidue = new WaitForSeconds(3f); 

    [Header("Game Stat")]
    private int playerKills;
    public int PlayerKills
    {
        get { return playerKills; }
        set 
        { 
            playerKills = value; 
            OnKills?.Invoke(playerKills);
        }
    }
    private int stage; 
    public int Stage
    {
        get { return stage; }
        set 
        { 
            stage = value; 
        }
    }
    public event UnityAction<int> StageChange; 
    public event UnityAction<int> OnKills;

    public void InitializeDefaultStat(PlayerStat stat)
    {
        moveSpeed = stat.normalSpeed;
        crouchSpeed = stat.crouchSpeed; 
        runSpeed = stat.runSpeed;
        maxHealth = stat.health;
        mouseSensitivity = stat.mouseSensitivity;
        health = maxHealth; 
    }

    public void InitializeWeaponStat(WeaponSO weaponSO)
    {
    }

    public void InitializeData()
    {
        health = maxHealth;
        playerKills = 0; 
    }

}
