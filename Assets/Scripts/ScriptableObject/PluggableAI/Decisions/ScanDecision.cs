using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[CreateAssetMenu(fileName = "Decision_Scan_", menuName = "PluggableAI/Decisions/Scan")]
public class ScanDecision : Decision
{
    //[SerializeField] float resetLock; 
    public override bool Decide(StateController controller)
    {
        bool targetVisible = Scan(controller);
        return targetVisible;
    }

    private bool Scan(StateController controller)
    {
        bool tempResult = controller.Sight.FindTarget();
        return tempResult;
    }
}
