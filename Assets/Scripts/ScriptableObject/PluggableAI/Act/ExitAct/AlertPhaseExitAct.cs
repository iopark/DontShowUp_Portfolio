using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_AlertPhase_", menuName = "PluggableAI/ExitAct/AlertPhaseAct")]
public class AlertPhaseExitAct : Act
{
    public override void Perform(StateController controller)
    {
        SetAlertState(controller);
    }

    private void SetAlertState(StateController controller)
    {
        AnimRequestSlip animRequestSlip = new AnimRequestSlip(AnimType, animTrigger);
        controller.Enemy.AnimationUpdate(animRequestSlip);
        controller.EnemyMover.ChangeMovementSpeed(controller.EnemyMover.NormalMoveSpeed);
        controller.EnemyMover.PatrolPoints.Clear(); 
        controller.Sight.ChangeSightByState(SightSensory.STATE.Normal);
    }
}

