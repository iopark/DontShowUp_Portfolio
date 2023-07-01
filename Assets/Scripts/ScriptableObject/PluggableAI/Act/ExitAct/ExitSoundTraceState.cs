using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_SoundTraceState_", menuName = "PluggableAI/ExitAct/SoundTraceState")]
public class ExitSoundTraceState : Act
{
    public override void Perform(StateController controller)
    {
        foreach(Action act in actionsToStop)
        {
            controller.ResetCoroutine(act.GetType().Name);
        }
    }

}
