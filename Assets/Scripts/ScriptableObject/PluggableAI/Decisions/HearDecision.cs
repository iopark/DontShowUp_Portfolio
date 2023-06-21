using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
[CreateAssetMenu(fileName = "Decision_Hear_", menuName = "PluggableAI/Decisions/Listen")]
public class HearDecision : Decision
{
    //Fixed Decision Making 
    public override bool Decide(StateController controller)
    {
        return controller.tracingStatus; 
    }
}
