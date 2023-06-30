using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_SearchState_", menuName = "PluggableAI/EnterAct/SearchState")]
public class EnterSearchState : Act
{
    public override void Perform(StateController controller)
    {
        InitializeSearchState(controller); 
    }

    private void InitializeSearchState(StateController controller)
    {
        controller.ResetPoints(); 
        controller.EnemyMover.PatrolCount = 0;
        controller.EnemyMover.PatrolIndex = 0;
        controller.EnemyMover.LookDir = controller.transform.forward;
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Normal); 
    }
}
