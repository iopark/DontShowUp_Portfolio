using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnteringAct_AlertPhase_", menuName = "PluggableAI/EnteringAct/AlertPhaseAct")]
public class AlertPhaseAct : Act
{
    public override void Perform(StateController controller)
    {
        SetAlertState(controller);
    }

    private void SetAlertState(StateController controller)
    {
        AnimRequestSlip animRequestSlip = new AnimRequestSlip(AnimType, animTrigger);
        controller.Enemy.AnimationUpdate(animRequestSlip);
        controller.EnemyMover.ChangeMovementSpeed(controller.EnemyMover.AlertMoveSpeed); 
    }
}
