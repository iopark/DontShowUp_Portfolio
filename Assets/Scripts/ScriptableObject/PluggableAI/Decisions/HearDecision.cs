using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
[CreateAssetMenu(fileName = "Decision_Fixed_Hear_", menuName = "PluggableAI/Fixed/Decisions/Listen")]
public class HearDecision : Decision
{
    //Fixed Decision Making 
    public override bool Decide(StateController controller)
    {
        //maybe 소리를 들었다고 비명을 지르는것도 하나의 방법일수도 있겠다. 
        return controller.Auditory.HaveHeard;
    }
}
