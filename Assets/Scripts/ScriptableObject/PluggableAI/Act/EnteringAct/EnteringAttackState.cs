using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "EnterAct_AttackState_", menuName = "PluggableAI/EnterAct/AttackState")]
public class EnteringAttackState : Act
{
    public override void Perform(StateController controller)
    {
        if (actionsToStop.Length < 0)
            return;
        foreach(Action action in actionsToStop)
        {
            controller.ResetCoroutine(action.GetType().Name);
        }
        controller.Sight.SetDirToPlayer(); 
    }

    //void SetDirToFacePlayer(StateController controller)
    //{
    //    controller.Sight.SetDirToPlayer(); 
    //}
}
