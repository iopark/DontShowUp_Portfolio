using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
[CreateAssetMenu(fileName = "Decision_Fixed_Hear_", menuName = "PluggableAI/Fixed/Decisions/Listen")]
public class HearDecision : Decision
{
    /// <summary>
    /// Return True Condition: 
    /// 1. If HaveHeard is true => Restart the Trace State. 
    /// False Condition: 
    /// 1. If HaveHeard is false => Remain In State. 
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public override bool Decide(StateController controller)
    {
        //maybe �Ҹ��� ����ٰ� ����� �����°͵� �ϳ��� ����ϼ��� �ְڴ�. 
        //Must Reset Trace Coroutine. 
        if (controller.Auditory.HaveHeard)
        {
            controller.Auditory.HaveHeard = false;
            return true; 
        }
        if (!controller.EnemyMover.IsTracingSound)
            return false;
        return false;
    }
}
