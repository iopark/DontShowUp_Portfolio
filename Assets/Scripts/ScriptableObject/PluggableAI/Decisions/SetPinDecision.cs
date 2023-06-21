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

        if (controller.patrolPoints.Count != controller.CurrentStat.patrolSize)
        {
            return false;
        }
        controller.ReversePatrolPoints();
        return true; 
    }
}
