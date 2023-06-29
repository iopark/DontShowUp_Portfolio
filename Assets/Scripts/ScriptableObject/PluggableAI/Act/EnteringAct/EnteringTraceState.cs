using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_TraceState_", menuName = "PluggableAI/EnterAct/TraceState")]
public class EnteringTraceState : Act
{
    public override void Perform(StateController controller)
    {
        InitializeTraceState(controller); 
    }

    private void InitializeTraceState(StateController controller)
    {
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert); 
    }
}
