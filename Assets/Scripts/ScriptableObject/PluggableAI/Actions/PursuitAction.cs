using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Action_Pursuit_", menuName = "PluggableAI/Actions/Pursuit")]
public class PursuitAction : Action
{
    [SerializeField] string coroutineKey; 
    [SerializeField] float dotThreshold = 0.98f;
    [SerializeField] float distanceThreshhold = 0.1f; 
    public override void Act(StateController controller)
    {
        Pursuit(controller);
    }

    private void Pursuit(StateController controller)
    {
        // 
        // if scanner has found the target, try to seek it out. 
        // since scanner will identify target to track and choose its direction, Vector3 target = controller.Sight.FindTarget();
        //if (controller.Sight.PlayerInSight == Vector3.zero)k
        //    return;

        Vector3 target = controller.Sight.PlayerLocked.position;
        controller.RunAndSaveForReset(ChaseTarget(controller), coroutineKey); 
    }

    IEnumerator ChaseTarget(StateController controller)
    {
        float distanceToTarget;
        Vector3 lookDir;
        Quaternion rotation; 
        while (controller.Sight.PlayerLocked != null)
        {
            distanceToTarget = Vector3.SqrMagnitude(controller.Sight.PlayerLocked.position - controller.transform.position);
            lookDir = controller.Sight.PlayerLocked.position - controller.transform.position;
            lookDir.y = 0f; 
            lookDir.Normalize();
            rotation = Quaternion.LookRotation(lookDir);
            while (Vector3.Dot(controller.transform.forward, lookDir) < dotThreshold)
            {
                controller.transform.rotation = Quaternion.Lerp(controller.transform.rotation, rotation, 0.5f);
                yield return null;
            }
            while (distanceToTarget > distanceThreshhold)
            {
                controller.EnemyMover.CharacterController.Move(lookDir * controller.CurrentSpeed * Time.deltaTime);
                yield return null;
            }

        }
    }
}
