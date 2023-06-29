using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_PatrolState_", menuName = "PluggableAI/ExitAct/PatrolState")]
public class ExitPatrolState : Act
{
    public override void Perform(StateController controller)
    {
        foreach(Action act in actionsToStop)
        {
            controller.ResetCoroutine(act.GetType().Name);
        }
        controller.EnemyMover.PatrolCount = 0; 
        controller.EnemyMover.PatrolIndex = 0;
    }

}
