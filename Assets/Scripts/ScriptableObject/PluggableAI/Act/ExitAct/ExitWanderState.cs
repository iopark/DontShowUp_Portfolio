using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ExitAct_WanderState_", menuName = "PluggableAI/ExitAct/WanderState")]
public class ExitWanderState : Act
{
    [SerializeField] string coroutineKey;

    public override void Perform(StateController controller)
    {
        controller.ResetCoroutine(coroutineKey);
    }

}
