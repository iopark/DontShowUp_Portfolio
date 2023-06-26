using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Enemy : MonoBehaviour, IHittable, IStrikable
{
    //Data in which should be clicked and dragged || shared through ResourceManager. 
    [SerializeField] protected EnemyData data;
    [SerializeField] protected SightSensory sight;
    [SerializeField] protected SoundSensory auditory;
    [SerializeField] public StateController controller;
    [SerializeField] public CharacterController characterController;
    [SerializeField] public Animator anim;

    [Header("Debug Purposes")]
    public bool debug;
    public bool tracingStatus;

    [Header("Default Abilities")]
    public Vector3[] tracePath;
    public int trackingIndex;

    #region Default Enemy Stats: Requires Refactoring 
    //TODO: Others to Refactor 
    private int health;
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

    private void FixedUpdate()
    {
        //TODO: Must respond to the Gravity 

    }
    protected virtual void Awake()
    {
        data = GameManager.Resource.Load<EnemyData>("Data/Zombie/PatrolZ/EnemyData_PatrolZ");
        sight = GetComponent<SightSensory>();
        auditory = GetComponent<SoundSensory>();
        controller = GetComponent<StateController>();
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        CurrentStat = data.AccessLevelData();
        GetCoreStat(); 
    }

    public void UponLevelUp()
    {
        CurrentLevel++;
        CurrentStat = data.AccessLevelData(CurrentLevel); 
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
    //Can be upgraded for the AdvancedZombie, able to take in weighted value of the graph to search for the other paths to attack the player (if possible)
    //For now, 소리가 들릴때마다 코루틴을 정지하고 입력받은 새로운 리스트 대로 이동하기 시작합니다. 
    //protected virtual void OnDrawGizmos()
    //{
    //    if (!debug && tracingStatus)
    //    {
    //        for (int i = 0; i < tracePath.Length; i++)
    //        {
    //            Gizmos.color = Color.black;
    //            Gizmos.DrawCube(tracePath[i], Vector3.one);

    //            if (i == trackingIndex)
    //            {
    //                Gizmos.DrawLine(transform.position, tracePath[i]);
    //            }
    //            else
    //            {
    //                Gizmos.DrawLine(tracePath[i - 1], tracePath[i]);
    //            }
    //        }
    //    }
    //}


    public void DoAction<T>(T bodyComponent, float timeInterval)
    {
        if (bodyComponent is EnemyMover)
        {
            //Try Following 
        }
        else if (bodyComponent is EnemyAttacker)
        {
            //Try this instead 
        }
    }
    public void TakeHit(int damage)
    {
        throw new System.NotImplementedException();
    }

    public void GiveDamage(IHittable target, int damage)
    {
        if (target == null)
            return;
        target.TakeHit(damage);
    }
}