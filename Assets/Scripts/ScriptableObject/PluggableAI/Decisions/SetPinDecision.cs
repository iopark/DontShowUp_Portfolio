using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_SetPin_", menuName = "PluggableAI/Decisions/SetPin")]
public class SetPinDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool pinPointSearchComplete = Search(controller);
        return pinPointSearchComplete;
    }

    private bool Search(StateController controller)
    {
        // if true, keep searching, setting pinpoints. 

        if (controller.patrolPoints.Count != controller.Enemy.CurrentStat.patrolSize)
        {
            return false;
        }
        controller.ReversePatrolPoints();
        //TODO: This can/should be refactored as inidividual prerequisite act
        controller.CurrentLookDir = controller.patrolPoints[0].Direction;
        return true;
    }
}
