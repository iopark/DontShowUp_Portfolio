using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_SearchState_", menuName = "PluggableAI/EnterAct/SearchState")]
public class EnteringSearchState : Act
{
    public override void Perform(StateController controller)
    {
        InitializeSearchState(controller); 
    }

    private void InitializeSearchState(StateController controller)
    {
        controller.EnemyMover.LookDir = controller.transform.forward;
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Normal); 
    }
}
