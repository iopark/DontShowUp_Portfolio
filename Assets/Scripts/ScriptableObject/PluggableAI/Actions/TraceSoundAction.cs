using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceSoundAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.CurrentSpeed != controller.Enemy.CurrentStat.alertMoveSpeed)
            controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert);
    }
}
