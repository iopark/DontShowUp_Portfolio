//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;

//[CreateAssetMenu(fileName = "Action_Patrol_", menuName = "PluggableAI/Actions/Patrol")]
//public class PatrolAction : Action
//{
//    [SerializeField] private Act defaultMove;
//    [SerializeField] private Act defaultRotate;
//    [SerializeField] private float patrolOffset;
//    public override void Act(StateController controller)
//    {
//        Patrol(controller);
//    }

//    private void Patrol(StateController controller)
//    {
//        Vector3 searchPoint = controller.patrolPoints[controller.PatrolIndex].worldPosition;
//        controller.CurrentLookDir = controller.patrolPoints[controller.PatrolIndex].Direction;
//        if (Vector3.Dot(controller.CurrentLookDir, controller.transform.forward) < 0.99)
//        {
//            defaultRotate.Perform(controller);
//            return;
//        }

//        defaultMove.Perform(controller);
//        if (ReachedDestination(controller.transform.position, searchPoint))
//            controller.PatrolIndex = (controller.PatrolIndex + 1 % controller.patrolPoints.Count); 
//    }
//    private bool ReachedDestination(Vector3 origin, Vector3 destination)
//    {
//        Vector3 delta = destination - origin;
//        //Vector3.Dot(delta, delta); 
//        return Vector3.Dot(delta, delta) < patrolOffset;
//    }
//}
