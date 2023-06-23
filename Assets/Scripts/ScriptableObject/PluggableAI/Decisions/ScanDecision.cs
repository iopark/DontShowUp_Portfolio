using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Scan_", menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Scan(controller);
        return targetVisible;
    }

    private bool Scan(StateController controller)
    {
        Vector3 findingTarget = controller.Sight.FindTarget();
        if (findingTarget != Vector3.zero)
        {
            controller.Sight.PlayerInSight = findingTarget;
            controller.Sight.SetDirToTargetForChase(findingTarget);
            return true; 
        }
        else
        {
            controller.Sight.PlayerInSight = Vector3.zero;
            return false;
        }
    }
}
