using System.Collections;
using UnityEngine;
[CreateAssetMenu(fileName = "Action_IdleWander_", menuName = "PluggableAI/Actions/Wander")]
public class IdleWanderAction : Action
{
    [SerializeField] private float wanderDistance; 
    [SerializeField] private Act defaultMove;
    [SerializeField] private Act defaultRotate;
    [SerializeField] private Act defaultWander;
    [SerializeField] private float dotThreshHold = .96f;
    [SerializeField] private float distanceThreshHold = 0.2f;

    public override string actionName => typeof(IdleWanderAction).Name;

    public override void Act(StateController controller)
    {
        //if (controller.EnemyMover.LookDir == Vector3.zero)
        //    defaultWander.Perform(controller); // set the LookDir to random Unit Vector
        //if (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < 0.95f)
        //{
        //    defaultRotate.Perform(controller);
        //    return;
        //}

        controller.RunAndSaveForReset(actionName, Wander(controller)); 
    }

    IEnumerator Wander(StateController controller)
    {
        float distanceToTarget; 
        //Vector3 lookDir;
        Vector3 destination; 
        while (true)
        {
            //lookDir = destination - controller.transform.position;
            //lookDir.y = controller.transform.position.y;
            //lookDir.Normalize();
            destination = controller.transform.forward * Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir); 
            distanceToTarget = Vector3.SqrMagnitude(destination - controller.transform.position);
            while (Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir) < dotThreshHold)
            {
                defaultRotate.Perform(controller); 
                yield return null;
            }
            if (distanceToTarget > distanceThreshHold)
            {
                defaultMove.Perform(controller);
                yield return null;
            }
            else
                defaultWander.Perform(controller); 
        }
    }
}
