using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Decision_#_Pursuit", menuName = "PluggableAI/Decisions/PursuitDecision")]
public class PursuitDecision : Decision
{
    // Look Decision 과 다른점. 
    // Pursuit를 계속할지 않할지에 대해서만 결정을 내려준다. 
    public override bool Decide(StateController controller)
    {
        return Pursuiting(controller);
    }

    private bool Pursuiting(StateController controller)
    {
        Vector3 target = controller.characterFov.FindTarget();
        if (target == Vector3.zero)
        {
            return false;
        }
        return true;
    }
}
