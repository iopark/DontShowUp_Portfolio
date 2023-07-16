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
                StageEnd?.Invoke(stage, false); 
            }
        }
    }
    public event UnityAction<int> OnHealthChange;
    public UnityAction GameEnd; //TODO: Link up the sound, UI for the player death. 

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

    #region Game Stage Related 
    private int stage; 
    public int Stage
    {
        get { return stage; }
        set 
        { 
            stage = value; 
            if (stage >= maxStage)
            {
                GameEnd?.Invoke(); 
            }
            else
                StageEnd?.Invoke(stage, true);
        }
    }

    private int maxStage; 
    public int MaxStage
    {
        get { return maxStage; }
        set { maxStage = value; }
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
    #endregion

    #region spawn Related
    //========================================================
    public int MaxZombie
    {
        get => CurrentGameData.maxZombie;
    }
    public float SpawnInterval
    {
        get => CurrentGameData.spawnTimer; 
    }
    #endregion
    public int TargetDiamonds { get; set; }
    public UnityAction<int, bool> StageEnd;

    //Update InGames 
    public UnityAction PauseGame; 
    public UnityAction<int> Harvested; 
    public UnityAction<int> NextStage;
    public UnityAction GamePause;

    #region Level Design Related; 
    //=========================================================

    public StagesData gameData;
    public SingleStage CurrentGameData
    {
        get
        {
            return gameData.StageLists[stage].stage;
        }
        set
        {
            CurrentGameData = value;
        }
    }
    private void Awake()
    {
        gameData = Resources.Load<StagesData>("Data/Game/Stages"); 
        stat = Resources.Load<PlayerStat>("Data/Player/Stat_Player"); 
        InitializeDefaultStat(stat);
        InitializeGameData();
        GameManager.Instance.GameSetup += InitializeStageData; 
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
    public void InitializeStageData()
    {
        // How do we make sure certain function is called upfront more than others do? 
        //TODO: Make Sure Stage is incremented eachtime stage is cleared. => Stage value should be increased by the game clear event; 
        int levelData;
        if (stage == 0)
        {
            levelData = 0; 
        }
        else
            levelData = stage - 1;
        this.CurrentGameData = gameData.StageLists[levelData].stage;
        this.TargetDiamonds = CurrentGameData.requiredDiamonds;
        health = maxHealth;
    }

    public void InitializeGameData()
    {
        this.stage = 0;
        this.CurrentGameData = gameData.StageLists[0].stage;
        this.diamond = 0; 
        this.TargetDiamonds = CurrentGameData.requiredDiamonds; 
        this.maxStage = gameData.StageLists.Length;
        health = maxHealth;
    }
    #endregion
}