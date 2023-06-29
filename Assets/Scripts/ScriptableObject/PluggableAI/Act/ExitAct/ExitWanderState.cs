using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_WanderState_", menuName = "PluggableAI/ExitAct/WanderState")]
public class ExitWanderState : Act
{
    public override void Perform(StateController controller)
    {
        foreach(Action act in actionsToStop)
        {
            controller.ResetCoroutine(actionsToStop.GetType().Name);
        }
    }

}
