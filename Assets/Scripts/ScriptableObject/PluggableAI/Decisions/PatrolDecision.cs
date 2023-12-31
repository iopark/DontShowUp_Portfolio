using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Patrol_", menuName = "PluggableAI/Decisions/Patrol")]
public class PatrolDecision : Decision
{
    [SerializeField] private int maxPatrolCount;
    [SerializeField] private float patrolOffset; // 목표지점까지 도착했다고 판별하는 조건 
    //목표지점에 도달했는지 파악하며 파악시에는 다시 patrol State 에 머무릅니다. 
    public override bool Decide(StateController controller)
    {
        return PointPatrol(controller);
        // 일차 목적지에 도착했으면 true 
        // 아직은 아니라면 false이다. 
    }
    //hlep me 
    private bool PointPatrol(StateController controller)
    {
        if (controller.EnemyMover.PatrolCount >= maxPatrolCount)
        {
            controller.EnemyMover.PatrolIndex = 0; 
            controller.ResetPoints(); 
            return true; // Return to the Search Pattern 
        }
        return false;
    }
}
