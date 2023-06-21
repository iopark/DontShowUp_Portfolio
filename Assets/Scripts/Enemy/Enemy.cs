using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
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

    #region Refactor variables 
    //TODO: Others to Refactor 
    private int health = 100;
    public int Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }
    private int damage;
    private int currentLevel = 0; 
    private int maxLevel; 
    public int Damage {  get { return damage; } set { damage = value; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    private float moveSpeed;

    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    private float alertMoveSpeed;

    public float AlertMoveSpeed
    {
        get { return alertMoveSpeed; }
        set { alertMoveSpeed = value; }
    }
    private float rotationSpeed;
    public float RotationSpeed
    {
        get { return rotationSpeed;}
        set { rotationSpeed = value; }
    }

    public float RotateSpeedDelta; 
    #endregion

    protected virtual void ImportEnemyData()
    {
        
        //TODO: Import relevent info for the sight, audiotory sense and more. 
    }

    //Can be upgraded for the AdvancedZombie, able to take in weighted value of the graph to search for the other paths to attack the player (if possible)
    //For now, 소리가 들릴때마다 코루틴을 정지하고 입력받은 새로운 리스트 대로 이동하기 시작합니다. 
    public virtual void ReactToSound(Vector3[] newPath)
    {
        tracingStatus = true; 
        StopAllCoroutines(); 
        StartCoroutine(FollowSound(newPath));
    }

    IEnumerator FollowSound(Vector3[] traceablePath)
    {
        tracePath = traceablePath;
        int trackingIndex = 0; 
        Vector3 currentWaypoint = traceablePath[0];
        while (true)
        {
            if (transform.position == currentWaypoint)
            {
                trackingIndex++;
                if (trackingIndex >= traceablePath.Length)
                {
                    tracingStatus = false; 
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            characterController.Move(currentWaypoint * moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

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
}


