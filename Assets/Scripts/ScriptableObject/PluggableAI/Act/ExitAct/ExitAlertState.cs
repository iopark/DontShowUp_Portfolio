using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_AlertState_", menuName = "PluggableAI/ExitAct/AlertState")]
public class ExitAlertState : Act
{
    public override void Perform(StateController controller)
    {
        //if (stopAllCoroutine)
        //    controller.ResetAllCoroutines();
        //else
        //    foreach(Action actionToStop in actionsToStop)
        //    {
        //        controller.StopCoroutine(actionToStop.GetType().Name);
        //    }
        AlertExitState(controller); 
    }
    private void AlertExitState(StateController controller)
    {
        AnimRequestSlip animRequestSlip = new AnimRequestSlip(AnimType, animTrigger, animBoolValue);
        controller.Enemy.AnimationUpdate(animRequestSlip);
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Normal);
    }
}
