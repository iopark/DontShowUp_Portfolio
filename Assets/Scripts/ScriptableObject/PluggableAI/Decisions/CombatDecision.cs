using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_ToCombat_", menuName = "PluggableAI/Decisions/ToCombat")]
public class CombatDecision : Decision
{
    public override bool Decide(StateController controller)
    {
        return ContestForRange(controller);
    }

    private bool ContestForRange(StateController controller)
    {
        bool result  = controller.Sight.AccessForAttackRange(); 
        if (!result)
        {
            controller.EnemyAttacker.StopAttack(); 
        }
        return result; 
    }
}
