using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "EnteringAct_NormalPhase_", menuName = "PluggableAI/EnteringAct/NormalPhaseAct")]
public class NormalPhaseAct : Act
{
    public override void Perform(StateController controller)
    {
        SetNormalPhase(controller);
    }

    public void SetNormalPhase(StateController controller)
    {
        controller.EnemyMover.CurrentSpeed = controller.EnemyMover.NormalMoveSpeed; 
    }
}
