using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat_", menuName = "Registry/EnemyInformation")]
public class EnemyStat : ScriptableObject
{
    public string _name;
    public int health;
    public float normalMoveSpeed;
    public float alertMoveSpeed;
    public float rotationSpeed;
    public int damage;
    public int thisLevel;
    public int maxLevel;
    public int attackRange;

    //Pertaining to Sight
    public float sightAngle;
    public float sightDepth;
    public float maxDepth;
    public float enemySightDepth;
    public float enemySightAngle; 

    //Others 
    public int stateSize;
    public int patrolSize;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    public void SyncCoreData(Enemy enemy)
    {
        enemy.Health = health;
        enemy.MaxLevel = maxLevel;
    }

    public void SyncSightData(SightSensory enemySight)
    {
        enemySight.Range = sightDepth;
        enemySight.Angle = sightAngle;
        enemySight.EnemyDetectRange = enemySightDepth;
        enemySight.EnemyDetectAngle = enemySightAngle; 
        enemySight.TargetMask = targetMask;
        enemySight.ObstacleMask = obstacleMask;
    }
    public void SyncMovementData(EnemyMover enemyMover)
    {
        enemyMover.NormalMoveSpeed = normalMoveSpeed;
        enemyMover.RotationSpeed = rotationSpeed;
        enemyMover.AlertMoveSpeed = alertMoveSpeed;
        enemyMover.RotationSpeed = rotationSpeed;
    }
}
