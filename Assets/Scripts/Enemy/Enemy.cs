using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable, IStrikable, IPausable
{
    //Data in which should be clicked and dragged || shared through ResourceManager. 
    [SerializeField] protected EnemyData data;
    [SerializeField] public StateController controller;
    [SerializeField] public EnemyAttacker enemyAttacker;
    [SerializeField] public EnemyMover enemyMover;
    [SerializeField] public Animator anim;

    [Header("Debug Purposes")]
    public bool debug;
    public bool tracingStatus;

    [Header("Default Abilities")]
    public Vector3[] tracePath;
    public int trackingIndex;

    #region Default Enemy Stats: Requires Refactoring 
    //TODO: Others to Refactor 
    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }
    private int damage;
    private int currentLevel; 
    private int maxLevel;
    public int Damage { get { return damage; } set { damage = value; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    private EnemyStat currentStat;
    public EnemyStat CurrentStat { get { return currentStat; } set { currentStat = value; } }
    #endregion

    WaitForSeconds returnToPool = new WaitForSeconds(5f); 

    protected virtual void Awake()
    {
        data = GameManager.Resource.Load<EnemyData>($"Data/Zombie/{gameObject.name}Data");
        controller = GetComponent<StateController>();
        anim = GetComponent<Animator>();
        CurrentStat = data.AccessLevelData();
        enemyAttacker = GetComponent<EnemyAttacker>();
        GetCoreStat(); 
    }

    public void UponLevelUp()
    {
        CurrentLevel++;
        CurrentStat = data.AccessLevelData(CurrentLevel); 
    }

    private void OnDisable()
    {
        anim.Rebind();
        Health = 100; 
    }
    public void GetCoreStat()
    {

        CurrentStat.SyncCoreData(this);
    }
    protected virtual void ImportEnemyData()
    {

    }
    public void AnimationUpdate(AnimRequestSlip animRequest)
    {
        //TODO: Each state should be able to update the Statemachine's Animation as well 
        //Where default is the Animation type trigger
        switch (animRequest.AnimType)
        {
            case AnimType.Trigger: 
                anim.SetTrigger(animRequest.animName); 
                break;
            case AnimType.Float: 
                anim.SetFloat(animRequest.animName, (float)animRequest.animFloat); 
                break;
            case AnimType.Bool: anim.SetBool(animRequest.animName, (bool)animRequest.animBool); 
                break;
        }
    }

    public void TakeHit(int damage)
    {
        health -= damage;
        // stop the attack simulation, play the take hit anim trigger; 
        AfterStrike(); 
    }

    public void GiveDamage(IHittable target, int damage)
    {
        if (target == null)
            return;
        target.TakeHit(damage);
    }

    public void Pause(float time)
    {
        enemyMover.CurrentSpeed = 0;
        anim.speed = 0;
        //Should trigger Resume button after certain interval; 
    }

    public void Resume()
    {
        enemyMover.CurrentSpeed = enemyMover.AlertMoveSpeed;
        anim.speed = 1;
    }

    public void AfterStrike()
    {
        // stop the attack simulation, play the take hit anim trigger; 
        anim.SetTrigger("TakeHit");
        enemyAttacker.StopAttack();
    }

    public void UponDeath()
    {
        StartCoroutine(Death()); 
    }
    Coroutine Freezer;
    IEnumerator Freeze(float time) 
    { 
        yield return new WaitForSeconds(time); 
        Resume(); 
    }
    IEnumerator Death()
    {
        yield return returnToPool;
        anim.SetBool("Death", false); 
        GameManager.Resource.Destroy(this.gameObject); 
    }

    //Any resets upon death?
}