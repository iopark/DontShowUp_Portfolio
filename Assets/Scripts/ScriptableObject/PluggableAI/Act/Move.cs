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

    /// <summary>
    /// Advanced move should move
    /// </summary>
    /// <param name="controller"></param>
    protected virtual void OnMove(StateController controller)
    {
        controller.EnemyMover.Mover();
    }

    protected virtual void Dash(StateController controller) { } 
}
