using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
[CreateAssetMenu(fileName = "Act_Move_", menuName = "PluggableAI/Act/Move")]
public class Move : Act
{
    public override void Perform(StateController controller)
    {
        OnMove(controller);
    }

    private void OnMove(StateController controller)
    {
        controller.EnemyMover.Mover(controller.CurrentLookDir);
    }

    protected virtual void Dash(StateController controller) { } 
}
