using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Patrol_", menuName = "PluggableAI/Decisions/Patrol")]
public class PatrolDecision : Decision
{
    [SerializeField] private int MaxPatrolCount; 
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
        // if 플레이어가 패트롤의 꼭지점에 도달하였을때에
        if (controller.PatrolIndex == controller.CurrentStat.patrolSize - 1)
        {
            controller.ReversePatrolPoints(); // 패트롤 포인트를 반대로 돌려서 진행한다. 
            controller.patrolCount++;
        }
        if (controller.patrolCount >= MaxPatrolCount)
            return true;
        return false; 
    }
}
