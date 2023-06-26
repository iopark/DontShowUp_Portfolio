using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExitAct : Act
{
    public override void Perform(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    private void SetAnim(StateController controller)
    {
        controller.Enemy.AnimationUpdate(new AnimRequestSlip(animType, animTrigger)); 
    }
}
