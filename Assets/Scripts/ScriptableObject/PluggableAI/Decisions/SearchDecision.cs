//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "SearchDecision", menuName = "PluggableAI/Decisions/Search")]
//public class SearchDecision : Decision
//{
//    [SerializeField] public float pinPointOffset;
//    [SerializeField] private LayerMask obstacle; 

//    public override bool Decide(StateController controller)
//    {
//        bool pinPointSearchComplete = Search(controller); 
//        return pinPointSearchComplete;
//    }

//    private bool Search(StateController controller)
//    {
//        // if true, keep searching, setting pinpoints. 
//        int index = controller.returnPoints.Count-1;

//        Vector3 destinationPoint = controller.returnPoints[index];
//        Vector3 delta = destinationPoint - controller.transform.position;
//        float distToTarget = Vector3.Dot(delta, delta); 
//        if (distToTarget < pinPointOffset)
//        {
//            controller.searchStatus = false;
//            return true;
//        }
//        else if (Physics.Raycast(controller.transform.position, destinationPoint.normalized, out RaycastHit hit, distToTarget, obstacle))
//        {
//            controller.returnPoints[index] = hit.point;
//            controller.searchStatus = false;
//            return true; 
//        }
//            return false; 
//        //핀포인트로 찍은 장소에 도착했다면 다음 핀포인트를 찍을수 있도록 확인해주는것이 맞겠다. 
//    }
//}
