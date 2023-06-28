using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_#_Pursuit", menuName = "PluggableAI/Decisions/PursuitDecision")]
public class PursuitDecision : Decision
{
    // Look Decision 과 다른점이 없다, 
    // Pursuit 은 ScanDecision 에 의거해서 진행해준다. 
    // Pursuit를 계속할지 않할지에 대해서만 결정을 내려준다. 
    [SerializeField] float resetTimer; 
    public override bool Decide(StateController controller)
    {
        return Pursuiting(controller);
    }

    private bool Pursuiting(StateController controller)
    {
        //// if player has been temporarily gone out of the sight, run the timer, 
        //if (controller.Sight.PlayerInSight == Vector3.zero) // the Scanner has lost to identify the target. 
        //{
        //    if (controller.Sight.CheckElapsedTime(resetTimer))
        //    {
        //        controller.Sight.PlayerLocked = null; // uncheck the locked state. would also stop the Coroutine of Pursuiting behaviour. 
        //        return false; // if time has elapsed and no player is no longer found, 
        //    }
        //    return true; // until the timer has set, keep tracking the locked target. 
        //}
        //return true; //controller.Sight.PlayerInSight != Vector3.zero;
        if (controller.Sight.PlayerLocked == null)
            return false;
        return true; 
    }
}
