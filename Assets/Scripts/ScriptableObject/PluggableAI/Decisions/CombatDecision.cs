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
        // if enemy is either in attack range or isAttacking, retain the attacking state. 
        // thus, exit to pursuit state only if enemy is not in attack range and have finished attacking. 
        if (result||controller.EnemyAttacker.IsAttacking)
            return true; 
        else
        {
            controller.EnemyAttacker.StopAttack();
            controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert);
            return false;
        }
    }
}
