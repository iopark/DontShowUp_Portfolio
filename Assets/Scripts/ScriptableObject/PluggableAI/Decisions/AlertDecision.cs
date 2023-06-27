using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Alert_", menuName = "PluggableAI/Decisions/Alert")]
public class AlertDecision : Decision
{
    [SerializeField] float resetTargetLockInterval; 
    public override bool Decide(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    private void FurtherScan(StateController controller)
    {
        if (controller.Sight.PlayerInSight == Vector3.zero)
        {

        }
    }

    private bool AttemptToTrack(StateController controller)
    {
        if (controller.Sight.AccessForPursuit() || !controller.Sight.CheckElapsedTime(resetTargetLockInterval))
            return true;
        controller.Sight.PlayerLocked = null;
        return false;
    }
}
