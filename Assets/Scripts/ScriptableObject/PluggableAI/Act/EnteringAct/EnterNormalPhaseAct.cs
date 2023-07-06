using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "EnterAct_NormalPhase_", menuName = "PluggableAI/EnterAct/NormalPhaseAct")]
public class EnterNormalPhaseAct : Act
{
    public override void Perform(StateController controller)
    {
        SetNormalPhase(controller);
    }

    public void SetNormalPhase(StateController controller)
    {
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Normal);  
    }
}
