using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Attack_", menuName = "PluggableAI/Decisions/Attack")]
public class AttackDecision : Decision
{
    [SerializeField] private float senseRange;
    [SerializeField] private float attackAngle;
    [SerializeField] private float attackRange; // This should be equivalent to the Attack's attackRange 
    [SerializeField] private LayerMask target;
    public override bool Decide(StateController controller)
    {
        return ContestForRange(controller);
    }

    private bool ContestForRange(StateController controller)
    {
        //Accessing proper range for the Attack 
        if (Physics.SphereCast(controller.transform.position, senseRange, controller.CurrentLookDir, out RaycastHit hit, senseRange))
        {
            controller.CurrentLookDir = (controller.transform.position - hit.point).normalized;
            //controller.EnemyAttacker
            return true;
        }
        return false;
    }
}
