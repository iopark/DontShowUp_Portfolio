//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "PatrolDecision", menuName = "PluggableAI/Decisions/Patrol")]
//public class PatrolDecision : Decision
//{
//    [SerializeField] private float patrolOffset; // ��ǥ�������� �����ߴٰ� �Ǻ��ϴ� ���� 
//    //��ǥ������ �����ߴ��� �ľ��ϸ� �ľǽÿ��� �ٽ� patrol State �� �ӹ����ϴ�. 
//    public override bool Decide(StateController controller)
//    {
//        return PointPatrol(controller); 
//        // ���� �������� ���������� true 
//        // ������ �ƴ϶�� false�̴�. 
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
