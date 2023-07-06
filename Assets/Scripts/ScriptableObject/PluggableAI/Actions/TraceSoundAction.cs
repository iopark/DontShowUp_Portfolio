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
        //TODO: perhaps any obstacles on the way?
        //TODO: perhaps leave any tracable path to follow?
        //TODO: perhaps the tracable path can work like a node in which can be determined by higher zombie => priority queue to determine a path to trace unto. 
    }
}
