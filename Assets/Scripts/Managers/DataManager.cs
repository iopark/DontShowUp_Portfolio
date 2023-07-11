using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : MonoBehaviour
{
    [Header("Player Stat")]
    PlayerStat stat; 
    private float moveSpeed;
    private float crouchSpeed;
    private float runSpeed;
    private float mouseSensitivity; 
    private int maxHealth; 
    public int MaxHealth
    {
        get { return maxHealth; }   
    }
    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            OnHealthChange?.Invoke(health);     
            if (health <= 0)
            {
                GameEnd?.Invoke(false); 
            }
        }
    }
    public event UnityAction<int> OnHealthChange;
    public UnityAction<bool> GameEnd; //TODO: Link up the sound, UI for the player death. 

    [Header("WeaponStat")]
    private int currentWeaponIndex; 
    public int CurrentWeaponIndex
    {
        get { return currentWeaponIndex; }
        set { currentWeaponIndex = value; }
    }

    public WeaponSO primaryWeapon; 
    public WeaponSO secondaryWeapon;

    private int meleeDamage;
    public int MeleeDamage
    {
        get { return meleeDamage; }
        set { meleeDamage = value; }
    }
    private int meleeFlank; 
    public int MeleeFlank
    {
        get => meleeFlank;
        private set { meleeFlank = value; }
    }

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
    private int diamond; 
    public int Diamond
    {
        get { return diamond; } 
        set
        {
            diamond = value;
            Harvested?.Invoke(diamond);
        }
    }


    public void ResetStatusData()
    {

    }
    //Update InGames 
    public UnityAction PauseGame; 
    public UnityAction<int> Harvested; 
    public UnityAction<int> StageChange;
    public UnityAction<int> NextStage; 
    public UnityAction<int> OnKills;

    private void Awake()
    {
        stat = Resources.Load<PlayerStat>("Data/Player/Stat_Player"); 
        InitializeDefaultStat(stat);
    }
    public void InitializeDefaultStat(PlayerStat stat)
    {
        moveSpeed = stat.normalSpeed;
        crouchSpeed = stat.crouchSpeed; 
        runSpeed = stat.runSpeed;
        maxHealth = stat.health;
        mouseSensitivity = stat.mouseSensitivity;
        meleeDamage = stat.meleeDamage;
        meleeFlank = stat.meleeFlankDamage; 
        health = maxHealth; 
    }
    public void InitializeData()
    {
        health = maxHealth;
        playerKills = 0; 
    }

}
