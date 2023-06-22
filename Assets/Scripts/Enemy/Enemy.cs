using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable, IStrikable
{
    //Data in which should be clicked and dragged || shared through ResourceManager. 
    [SerializeField] protected EnemyData data;
    [SerializeField] protected SightSensory sight;
    [SerializeField] protected SoundSensory auditory;
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

    protected virtual void Awake()
    {
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

    //Can be upgraded for the AdvancedZombie, able to take in weighted value of the graph to search for the other paths to attack the player (if possible)
    //For now, 소리가 들릴때마다 코루틴을 정지하고 입력받은 새로운 리스트 대로 이동하기 시작합니다. 
    protected virtual void OnDrawGizmos()
    {
        if (!debug && tracingStatus)
        {
            for (int i = 0; i < tracePath.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(tracePath[i], Vector3.one);

                if (i == trackingIndex)
                {
                    Gizmos.DrawLine(transform.position, tracePath[i]);
                }
                else
                {
                    Gizmos.DrawLine(tracePath[i - 1], tracePath[i]);
                }
            }
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