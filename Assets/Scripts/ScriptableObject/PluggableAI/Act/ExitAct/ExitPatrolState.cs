using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_PatrolState_", menuName = "PluggableAI/ExitAct/PatrolState")]
public class ExitPatrolState : Act
{
    [SerializeField] Action ActionToCease;

    public override void Perform(StateController controller)
    {
        //controller.ResetCoroutine(ActionToCease.GetType().Name);
        controller.ResetCoroutine(ActionToCease.GetType().Name); 
        //Optional, probably not best implemented in the Advanced Zombie. 
        controller.EnemyMover.PatrolCount = 0; 
        controller.EnemyMover.PatrolIndex = 0;
    }

}
