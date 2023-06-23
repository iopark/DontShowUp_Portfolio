using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack_", menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        controller.EnemyAttacker.DefaultAttack.Perform();
    }

    private void PerformAttack(StateController controller)
    {
        //better to do so in the coroutine?
        //controller.EnemyMover.CurrentSpeed = 0f; 
    }
}
