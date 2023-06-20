using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Data in which should be clicked and dragged || shared through ResourceManager. 
    [SerializeField] protected EnemyData data;
    [SerializeField] protected SightSensory sight;
    [SerializeField] protected SoundSensory auditory;

    [Header("Debug Purposes")]
    public bool debug;
    public bool tracingStatus;

    [Header("Default Abilities")]
    public Vector3[] tracePath;
    public int trackingIndex;

    //TODO: Others to Refactor 
    private int damage;
    private int currentLevel = 0; 
    private int maxLevel; 
    public int Damage {  get { return damage; } set { damage = value; } }
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }
    public int CurrentLevel { get { return currentLevel; } set { currentLevel = value; } }

    private float moveSpeed;
    private float rotationSpeed; 
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    public float RotationSpeed
    {
        get { return rotationSpeed;}
        set { rotationSpeed = value; }
    }

    protected virtual void ImportEnemyData()
    {
        
        //TODO: Import relevent info for the sight, audiotory sense and more. 
    }

    //Can be upgraded for the AdvancedZombie, able to take in weighted value of the graph to search for the other paths to attack the player (if possible)
    public virtual void ReactToSound(Vector3[] newPath)
    {
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
                    yield break;
                }
                currentWaypoint = traceablePath[trackingIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, moveSpeed * Time.deltaTime);
            yield return null;

        }
    }

    private void OnDrawGizmos()
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
}


