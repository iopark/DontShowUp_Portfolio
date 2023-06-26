using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack_", menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        PerformAttack(controller);
    }

    private void PerformAttack(StateController controller)
    {
        //better to do so in the coroutine?
        //controller.EnemyMover.CurrentSpeed = 0f; 
        controller.EnemyAttacker.TryStrike(); 
    }
}
