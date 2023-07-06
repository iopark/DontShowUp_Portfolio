using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_AttackState_", menuName = "PluggableAI/EnterAct/AttackState")]
public class EnterAttackState : Act
{
    public override void Perform(StateController controller)
    {
        controller.Sight.SetDirToPlayer();
        if (actionsToStop.Length < 0)
            return;
        foreach(Action action in actionsToStop)
        {
            controller.ResetCoroutine(action.GetType().Name);
        }
    }

    //void SetDirToFacePlayer(StateController controller)
    //{
    //    controller.Sight.SetDirToPlayer(); 
    //}
}
