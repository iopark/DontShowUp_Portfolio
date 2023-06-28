using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_AttackState_", menuName = "PluggableAI/ExitAct/AttackState")]
public class ExitAttackState : Act
{
    public override void Perform(StateController controller)
    {
        if (actionsToStop.Length > 0)
        foreach (string key in actionsToStop)
        {
            controller.ResetCoroutine(key);
        }
        controller.EnemyMover.ChangeMovementSpeed(controller.EnemyMover.AlertMoveSpeed); 
    }
}
