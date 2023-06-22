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
        //maybe �Ҹ��� ����ٰ� ����� �����°͵� �ϳ��� ����ϼ��� �ְڴ�. 
        return controller.Auditory.HaveHeard;
    }
}
