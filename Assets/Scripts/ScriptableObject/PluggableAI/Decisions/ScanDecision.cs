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
        controller.Sight.PlayerInSight = controller.Sight.FindTarget();
        if (controller.Sight.PlayerInSight != Vector3.zero)
        {
            //Do this somewhere else. controller.Sight.SetDirToTargetForChase(findingTarget);
            return true; 
        }
        else if (controller.Sight.PlayerInSight == Vector3.zero && controller.Sight.PlayerLocked != null)
        {
            ////TODO: shoot a ray to the player to see if he is still somewhere within the reach 
            //if (controller.Sight.AccessForPursuit())
            //    return true;
            //if (controller.Sight.CheckElapsedTime(resetLock))
            //    return false; 
            //controller.Sight.PlayerInSight = Vector3.zero;
            //return false;
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
