using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_TraceSound_", menuName = "PluggableAI/Actions/TraceSound")]
public class TraceSoundAction : Action
{
    public override string actionName => typeof(TraceSoundAction).Name;

    public override void Act(StateController controller)
    {
        if (controller.EnemyMover.CurrentSpeed != controller.Enemy.CurrentStat.alertMoveSpeed)
            controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert);
        //TraceSound(controller); 
    }

    public void TraceSound(StateController controller)
    {

    }
}
