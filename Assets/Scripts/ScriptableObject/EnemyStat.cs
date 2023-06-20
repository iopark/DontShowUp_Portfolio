using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat_", menuName = "Registry/EnemyInformation")]
public class EnemyStat : ScriptableObject
{
    public string name; 
    public float moveSpeed;
    public float rotationSpeed; 
    public int damage;
    public int thisLevel; 
    public int maxLevel; 

    //Pertaining to Sight
    public float sightAngle;
    public float sightDepth;

    //Others 
    public int stateSize; 
    public int patrolSize;
    public LayerMask obstacleMask;
    public LayerMask targetMask; 

    public void SyncSightData(SightSensory enemySight)
    {
        enemySight.Range = sightDepth; 
        enemySight.Angle = sightAngle;
        enemySight.TargetMask = targetMask;
        enemySight.ObstacleMask = obstacleMask;
    }

    public void SyncPhysicalData(Enemy enemy)
    {
        enemy.Damage = damage;
        enemy.MaxLevel = maxLevel;
        enemy.MoveSpeed = moveSpeed;
        enemy.RotationSpeed = rotationSpeed;
    }
}
