using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    //Data in which should be clicked and dragged || shared through ResourceManager. 
    [SerializeField] protected EnemyData data;
    [SerializeField] public StateController controller;
    [SerializeField] public EnemyAttacker enemyAttacker;
    [SerializeField] public Animator anim;


    #region Default Enemy Stats
    [SerializeField] private int health;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }

    private int currentLevel; 
    private int maxLevel;
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
    
    IEnumerator Death()
    {
        yield return returnToPool;
        anim.SetBool("Death", false); 
        GameManager.Resource.Destroy(this.gameObject); 
    }

    #region DEBUGGING PURPOSES
    [Header("Debug Purposes")]
    public bool debug;
    public bool tracingStatus;
    #endregion

    #region Stop Freeze System 
    //Coroutine Freezer;
    //IEnumerator Freeze(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    Resume();
    //}

    //public void Pause(float time)
    //{
    //    enemyMover.CurrentSpeed = 0;
    //    anim.speed = 0;
    //    //Should trigger Resume button after certain interval; 
    //}

    //public void Resume()
    //{
    //    enemyMover.CurrentSpeed = enemyMover.AlertMoveSpeed;
    //    anim.speed = 1;
    //}
    #endregion
}