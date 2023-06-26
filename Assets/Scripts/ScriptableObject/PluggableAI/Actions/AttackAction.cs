using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Attack_", menuName = "PluggableAI/Actions/Attack")]
public class AttackAction : Action
{
    public override void Act(StateController controller)
    {
        // must either call to trigger the attackcoroutines, 
        controller.EnemyAttacker.TryStrike(); 
    }
}
