using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
[CreateAssetMenu(fileName = "Decision_Trace_", menuName = "PluggableAI/Decisions/TraceSound")]
public class TraceDecision : Decision
{
    /// <summary>
    /// If true, remain in TraceSound State. 
    /// 
    /// </summary>
    /// <param name="controller"></param>
    /// <returns></returns>
    public override bool Decide(StateController controller)
    {
        return controller.EnemyMover.IsTracingSound; 
    }
}
