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
        controller.RunAndSaveForReset(actionName, Wander(controller)); 
    }

    IEnumerator Wander(StateController controller)
    {
        float distanceToTarget; 
        //Vector3 lookDir;
        Vector3 destination;
        destination = controller.EnemyMover.LookDir * wanderDistance;
        while (true)
        {
             //controller.transform.forward * Vector3.Dot(controller.transform.forward, controller.EnemyMover.LookDir); 
            distanceToTarget = Vector3.SqrMagnitude(destination - controller.transform.position);
            defaultRotate.Perform(controller); 
            if (distanceToTarget > distanceThreshHold)
            {
                defaultMove.Perform(controller);
            }
            else
            {
                defaultWander.Perform(controller);
                destination = controller.EnemyMover.LookDir * wanderDistance;
            }

            yield return null;
        }
    }
}
