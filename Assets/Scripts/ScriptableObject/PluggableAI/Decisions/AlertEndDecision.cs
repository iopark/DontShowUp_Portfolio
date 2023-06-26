using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_AlertEnd_", menuName = "PluggableAI/Decisions/EndAlert")]
public class AlertEndDecision : Decision
{
    // how many times should one rotate ?
    [SerializeField] float alertTimes; 
    public override bool Decide(StateController controller)
    {
        return false; 
    }

    private bool IterateScanning(StateController controller)
    {
        if (controller.EnemyMover.CheckElapsedTime(alertTimes))
            return true;
        return false;
    }
}
