using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Align_", menuName = "PluggableAI/Decisions/Align")]
public class AlignDecision : Decision
{
    [SerializeField] private LayerMask wallLayer;
    public override bool Decide(StateController controller)
    {
        return Align(controller);
    }
    private bool Align(StateController controller)
    {
        if (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) > 0.99f)
        {
            return true;
        }
        //this should return false and continue to do so, 
        return false;
    }
}
