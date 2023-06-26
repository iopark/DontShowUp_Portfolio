using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_ToCombat_", menuName = "PluggableAI/Decisions/ToCombat")]
public class CombatDecision : Decision
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
        return controller.Sight.AccessForAttackRange(); 
    }
}
