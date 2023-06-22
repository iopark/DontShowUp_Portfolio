using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Patrol_", menuName = "PluggableAI/Decisions/Patrol")]
public class PatrolDecision : Decision
{
    [SerializeField] private int MaxPatrolCount; 
    [SerializeField] private float patrolOffset; // ��ǥ�������� �����ߴٰ� �Ǻ��ϴ� ���� 
    //��ǥ������ �����ߴ��� �ľ��ϸ� �ľǽÿ��� �ٽ� patrol State �� �ӹ����ϴ�. 
    public override bool Decide(StateController controller)
    {
        return PointPatrol(controller);
        // ���� �������� ���������� true 
        // ������ �ƴ϶�� false�̴�. 
    }
    //hlep me 
    private bool PointPatrol(StateController controller)
    {
        // if �÷��̾ ��Ʈ���� �������� �����Ͽ�������
        if (controller.PatrolIndex == controller.CurrentStat.patrolSize - 1)
        {
            controller.ReversePatrolPoints(); // ��Ʈ�� ����Ʈ�� �ݴ�� ������ �����Ѵ�. 
            controller.patrolCount++;
        }
        if (controller.patrolCount >= MaxPatrolCount)
            return true;
        return false; 
    }
}
