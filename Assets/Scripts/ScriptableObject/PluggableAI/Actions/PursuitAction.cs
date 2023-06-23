using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Pursuit")]
public class PursuitAction : Action
{
    public override void Act(StateController controller)
    {
        Pursuit(controller);
    }

    private void Pursuit(StateController controller)
    {
        // if scanner has found the target, try to seek it out. 
        Vector3 target = controller.Sight.FindTarget();
        if (controller.Sight.PlayerInSight == Vector3.zero)
            return;
        Vector3 lookDir = (target - controller.transform.position).normalized;
        lookDir.y = controller.transform.position.y;
        controller.transform.rotation = Quaternion.LookRotation(lookDir, Vector3.up);
        controller.EnemyMover.Mover(lookDir * speed * Time.deltaTime);
        controller.anim.SetBool("Walk Forward", true);
    }
}
