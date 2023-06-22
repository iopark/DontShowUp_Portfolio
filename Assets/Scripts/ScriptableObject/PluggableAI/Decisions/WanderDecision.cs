using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Wander_", menuName = "PluggableAI/Decisions/Wander")]
public class WanderDecision : Decision
{
    //Wander State Ending Decision, should be concluded if a wall is found, leading to the Align State 
    [SerializeField] private float wallDetectionRange;
    [SerializeField] private LayerMask wallLayer;
    public override bool Decide(StateController controller)
    {
        return WanderEnd(controller);
    }
    private bool WanderEnd(StateController controller)
    {
        if (Physics.Raycast(controller.transform.position, controller.ForwardVector, wallDetectionRange, wallLayer))
        {
            return true;
        }
        return false;
    }
}
