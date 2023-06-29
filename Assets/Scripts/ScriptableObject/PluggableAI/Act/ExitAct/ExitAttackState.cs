using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_AttackState_", menuName = "PluggableAI/ExitAct/AttackState")]
public class ExitAttackState : Act
{
    //[SerializeField] Action actionToResume; 
    public override void Perform(StateController controller)
    {
        //controller.ResetCoroutine(skillToStop.GetType().Name);
        if (actionsToStop.Length > 0)
            foreach (Action action in actionsToStop)
            {
                controller.ResetCoroutine(action.GetType().Name);
            }
        controller.EnemyMover.ChangeMovementSpeed(MoveState.Alert); 
    }
}
