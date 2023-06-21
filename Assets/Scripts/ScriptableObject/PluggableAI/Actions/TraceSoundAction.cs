using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceSoundAction : Action
{
    public override void Act(StateController controller)
    {
        if (controller.CurrentSpeed != controller.CurrentStat.alertMoveSpeed) 
            controller.CurrentSpeed = controller.CurrentStat.alertMoveSpeed;
    }
}
