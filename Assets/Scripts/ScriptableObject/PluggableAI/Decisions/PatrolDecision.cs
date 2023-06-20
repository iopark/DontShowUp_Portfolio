//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "PatrolDecision", menuName = "PluggableAI/Decisions/Patrol")]
//public class PatrolDecision : Decision
//{
//    [SerializeField] private float patrolOffset; // 목표지점까지 도착했다고 판별하는 조건 
//    //목표지점에 도달했는지 파악하며 파악시에는 다시 patrol State 에 머무릅니다. 
//    public override bool Decide(StateController controller)
//    {
//        return PointPatrol(controller); 
//        // 일차 목적지에 도착했으면 true 
//        // 아직은 아니라면 false이다. 
//    }
//    //hlep me 
//    private bool PointPatrol(StateController controller)
//    {
//        Vector3 origin = controller.transform.position;
//        Vector3 destination = controller.returnPoints[controller.patrolIndex];
//        //TODO: if reached destination, set the value for next search point 
//        if (ReachedDestination(origin, destination))
//        {
//            if (controller.patrolIndex == controller.returnPoints.Count -1)
//            {
//                controller.returnPoints.Reverse();
//                controller.patrolCount++; 
//            }
//            controller.patrolIndex = controller.patrolIndex + 1 % controller.returnPoints.Count;
//            controller.patrolStatus = false; 
//            return true;
//        }
//        return false;
//        //return controller.returnPoints.Count >= searchLimit; 
//        // if true, return to Search State 
//        // if false, remain in state 
//    }

//    private bool ReachedDestination(Vector3 origin, Vector3 destination)
//    {
//        Vector3 delta = destination - origin;
//        float result = delta.sqrMagnitude; 
//            //Vector3.Dot(delta, delta); 
//        return Vector3.Dot(delta, delta) < patrolOffset;
//    }
//}
