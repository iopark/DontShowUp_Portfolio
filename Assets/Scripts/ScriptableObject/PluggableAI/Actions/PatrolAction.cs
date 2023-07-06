using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Patrol_", menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override string actionName => typeof(PatrolAction).Name;
    [SerializeField] private Act defaultMove;
    [SerializeField] private Act defaultRotate;
    [SerializeField] private float patrolOffset;
    [SerializeField] private float dotThreshHold; 
    public override void Act(StateController controller)
    {
        Patrol(controller);
    }

    private void Patrol(StateController controller)
    {
        controller.RunAndSaveForReset(actionName, MoveToDestination(controller)); 
    }
    private void UpdatePatrolIndex(StateController controller)
    {
        controller.EnemyMover.PatrolIndex = ((controller.EnemyMover.PatrolIndex + 1) % controller.EnemyMover.PatrolPoints.Count);
        if (controller.EnemyMover.PatrolIndex == controller.Enemy.CurrentStat.patrolSize - 1)
        {
            controller.ReversePatrolPoints(); // 패트롤 포인트를 반대로 돌려서 진행한다. 
            controller.EnemyMover.PatrolCount++;
        }
    }

    IEnumerator MoveToDestination(StateController controller)
    {
        int index;
        Vector3 destination;
        float distanceToTarget;
        while (true)
        {
            index = controller.EnemyMover.PatrolIndex;
            destination = controller.EnemyMover.PatrolPoints[index].worldPosition;
            distanceToTarget = Vector3.SqrMagnitude(destination - controller.transform.position);

            Vector3 lookDir = destination - controller.transform.position;
            lookDir.y = 0f;
            lookDir.Normalize();
            controller.Sight.SetDirToLook(lookDir);
            defaultRotate.Perform(controller);
            defaultMove.Perform(controller);
            if (distanceToTarget < patrolOffset)
                UpdatePatrolIndex(controller);
            yield return null;
        }
    }
}
