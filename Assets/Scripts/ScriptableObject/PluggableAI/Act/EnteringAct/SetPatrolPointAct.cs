using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnteringAct_SetPatrolPoint_", menuName = "PluggableAI/EnteringAct/SetPatrolPoint")]
public class SetPatrolPointAct : Act
{
    public override void Perform(StateController controller)
    {
        SetPatrolPoint(controller);
    }

    private void SetPatrolPoint(StateController controller)
    {
        controller.PatrolIndex = 0;
        controller.EnemyMover.LookDir = controller.PatrolPoints[0].Direction;
        controller.EnemyMover.Rotator(controller.EnemyMover.LookDir); 
    }
}
