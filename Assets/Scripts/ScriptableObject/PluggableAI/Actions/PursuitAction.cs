using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Pursuit_", menuName = "PluggableAI/Actions/Pursuit")]
public class PursuitAction : Action
{
    public override void Act(StateController controller)
    {
        Pursuit(controller);
    }
    /// <summary>
    /// If PlayerInSight no longer exists, will simply return, and evaluate whether it is appropriate to be in the pursuit state. 
    /// </summary>
    /// <param name="controller"></param>
    private void Pursuit(StateController controller)
    {
        // 
        // if scanner has found the target, try to seek it out. 
        // since scanner will identify target to track and choose its direction, Vector3 target = controller.Sight.FindTarget();
        if (controller.Sight.LockInTarget != null)
            controller.EnemyMover.Chase(); 
        controller.EnemyMover.Mover();
    }
}
