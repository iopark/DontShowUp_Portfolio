using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decision_Wander_", menuName = "PluggableAI/Decisions/Wander")]
public class WanderDecision : Decision
{
    //Wander State Ending Decision, should be concluded if a wall is found, leading to the Align State 
    
    [SerializeField] private LayerMask wallLayer; 
    public override bool Decide(StateController controller)
    {
        return WanderEnd(controller); 
    }
    private bool WanderEnd(StateController controller)
    {
        RaycastHit hit;
        if (Physics.Raycast(controller.transform.position, dir, out hit, controller.CurrentStat.sightDepth, wallLayer))
        {
            return true; 
        }
        return false; 
    }

    //public void ResetFixToDir()
    //{
    //    controller.FixToDir = Vector3.zero; 
    //}
}
