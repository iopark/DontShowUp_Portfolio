using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_SetPatrolPoint_", menuName = "PluggableAI/EnterAct/SetPatrolPoint")]
public class EnterSetPatrolPointAct : Act
{
    public override void Perform(StateController controller)
    {
        SetPatrolPoint(controller);
    }

    private void SetPatrolPoint(StateController controller)
    {
        controller.EnemyMover.PatrolIndex = 0;
        controller.Sight.SetLookDirToPos(controller.EnemyMover.PatrolPoints[0].worldPosition);
        controller.EnemyMover.Rotator();
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Normal); 
    }
}
