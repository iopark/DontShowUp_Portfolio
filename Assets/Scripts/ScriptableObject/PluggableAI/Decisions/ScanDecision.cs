using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Scan_", menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    [SerializeField] float resetLock; 
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Scan(controller);
        return targetVisible;
    }

    private bool Scan(StateController controller)
    {
        bool tempResult = controller.Sight.FindTarget();
        if (!tempResult)
        {
            Debug.Log(controller.Sight.PlayerInSight); 
            //Do this somewhere else. controller.Sight.SetDirToTargetForChase(findingTarget);
            return true; 
        }
        else if (controller.Sight.PlayerInSight == Vector3.zero && controller.Sight.PlayerLocked != null)
        {
            return AttemptToTrack(controller);
        }
        return false;
    }

    private bool AttemptToTrack(StateController controller)
    {
        if (controller.Sight.AccessForPursuit() || !controller.Sight.CheckElapsedTime(resetLock))
            return true;
        controller.Sight.PlayerLocked = null;
        return false; 
    }
}
