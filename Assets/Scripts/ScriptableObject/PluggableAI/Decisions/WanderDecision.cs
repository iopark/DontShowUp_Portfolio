using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Wander_", menuName = "PluggableAI/Decisions/Wander")]
public class WanderDecision : Decision
{
    //Wander State Ending Decision, should be concluded if a wall is found, leading to the Align State 
    [SerializeField] Action ActionToEnd; 
    [SerializeField] private float wanderPeriod;
    public override bool Decide(StateController controller)
    {
        return WanderEnd(controller);
    }
    private bool WanderEnd(StateController controller)
    {
        if (controller.EnemyMover.CheckElapsedTime(wanderPeriod))
        {
            //TODO: Stop the Coroutine for the IdleWanderActions 
            controller.ResetCoroutine(ActionToEnd.actionName); 
            return true;
        }
        return false;
    }
}
