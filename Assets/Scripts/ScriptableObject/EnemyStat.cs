using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stat_", menuName = "Registry/EnemyInformation")]
public class EnemyStat : ScriptableObject
{
    public string name;
    public int health;
    public float moveSpeed;
    public float alertMoveSpeed;
    public float rotationSpeed;
    public int damage;
    public int thisLevel;
    public int maxLevel;
    public int attackRange;

    //Pertaining to Sight
    public float sightAngle;
    public float sightDepth;

    //Others 
    public int stateSize;
    public int patrolSize;
    public LayerMask obstacleMask;
    public LayerMask targetMask;

    public void SyncCoreData(Enemy enemy)
    {
        enemy.name = $"{name} + {thisLevel}";
        enemy.Health = health;
        enemy.MaxLevel = maxLevel;
    }

    public void SyncSightData(SightSensory enemySight)
    {
        enemySight.Range = sightDepth;
        enemySight.Angle = sightAngle;
        enemySight.TargetMask = targetMask;
        enemySight.ObstacleMask = obstacleMask;
    }

    public void SyncCombatData(EnemyAttacker attacker)
    {
        //This is moved to Scriptable Abilities 
    }


    public void SyncMovementData(EnemyMover enemyMover)
    {
        enemyMover.MoveSpeed = moveSpeed;
        enemyMover.RotationSpeed = rotationSpeed;
        enemyMover.AlertMoveSpeed = alertMoveSpeed;
        enemyMover.RotationSpeed = rotationSpeed;

    }
}
