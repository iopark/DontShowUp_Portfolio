using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Patrol_", menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    [SerializeField] private Act defaultMove;
    [SerializeField] private float patrolOffset;
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        Vector3 searchPoint = controller.EnemyMover.PatrolPoints[controller.PatrolIndex].worldPosition;
        Vector3 toPatrolPoint = (searchPoint - controller.EnemyMover.transform.position).normalized;
        if (toPatrolPoint == Vector3.zero)
        {
            controller.EnemyMover.Rotator(controller.EnemyMover.PatrolPoints[controller.PatrolIndex].Direction);
            controller.EnemyMover.LookDir = controller.EnemyMover.PatrolPoints[controller.PatrolIndex].Direction;
        }
        controller.EnemyMover.AlignDir = toPatrolPoint;
        //controller.EnemyMover.LookDir = controller.EnemyMover.PatrolPoints[controller.PatrolIndex].Direction;
        controller.EnemyMover.currentDestination = searchPoint;
        if (Vector3.Dot(controller.EnemyMover.AlignDir, controller.ForwardVector) < 0.98 && controller.EnemyMover.AlignDir != Vector3.zero)
        {
            if (controller.EnemyMover.AlignDir == Vector3.zero)
            {
                controller.EnemyMover.Rotator(controller.EnemyMover.PatrolPoints[controller.PatrolIndex].Direction);
                controller.EnemyMover.LookDir = controller.EnemyMover.PatrolPoints[controller.PatrolIndex].Direction; 
                return;
            }
                //return;
            controller.EnemyMover.Rotator(controller.EnemyMover.AlignDir); 
            return;
        }
        else
        {
            controller.EnemyMover.AlignDir = Vector3.zero; 
        }

        defaultMove.Perform(controller);
        if (ReachedDestination(controller.transform.position, searchPoint))
        {
            controller.EnemyMover.LookDir = controller.EnemyMover.PatrolPoints[controller.EnemyMover.PatrolIndex].Direction;
            controller.EnemyMover.Rotator(controller.EnemyMover.LookDir);
            controller.EnemyMover.PatrolIndex = ((controller.EnemyMover.PatrolIndex + 1) % controller.PatrolPoints.Count);
            if (controller.EnemyMover.PatrolIndex == controller.Enemy.CurrentStat.patrolSize - 1)
            {
                controller.ReversePatrolPoints(); // 패트롤 포인트를 반대로 돌려서 진행한다. 
                controller.patrolCount++;
            }
            Debug.Log(controller.PatrolIndex);
        }


    }
    private bool ReachedDestination(Vector3 origin, Vector3 destination)
    {
        Vector3 delta = destination - origin;
        //Vector3.Dot(delta, delta); 
        return Vector3.Dot(delta, delta) < patrolOffset;
    }
}
