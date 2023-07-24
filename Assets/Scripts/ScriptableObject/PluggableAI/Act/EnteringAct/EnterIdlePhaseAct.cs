using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnterAct_IdlePhase_", menuName = "PluggableAI/EnterAct/IdlePhaseAct")]
public class EnterIdlePhaseAct : Act
{
    public override void Perform(StateController controller)
    {
        SetAlertState(controller);
    }

    private void SetAlertState(StateController controller)
    {
        controller.ResetAllCoroutines();
        AnimRequestSlip animRequestSlip = new AnimRequestSlip(AnimType, animTrigger, animBoolValue);
        controller.Enemy.AnimationUpdate(animRequestSlip);
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Idle);
    }
}
