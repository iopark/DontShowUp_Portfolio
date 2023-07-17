using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Alert_", menuName = "PluggableAI/Decisions/Alert")]
public class AlertDecision : Decision
{
    [SerializeField] float resetTargetLockInterval; 
    public override bool Decide(StateController controller)
    {
        return FurtherScan(controller);
    }

    private bool FurtherScan(StateController controller)
    {
        return AttemptToTrack(controller); 
    }

    private bool AttemptToTrack(StateController controller)
    {
        if (controller.Sight.AccessForPursuit() || !controller.Sight.CheckElapsedTime(resetTargetLockInterval))
            return true;
        controller.Sight.PlayerLocked = null;
        return false;
    }
}
