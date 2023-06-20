//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "PatrolCompleteDecision", menuName = "PluggableAI/Decisions/PatrolComplete")]
//public class PatrolCompleteDecision : Decision
//{
//    [SerializeField] public int maxSearchLimit;  

//    public override bool Decide(StateController controller)
//    {
//        if (maxSearchLimit != controller.returnPoints.Count)
//            maxSearchLimit = controller.returnPoints.Count;
//        bool pinPointingComplete = FinishPatrol(controller); 
//        if (pinPointingComplete)
//        {
//            controller.returnPoints.Clear();
//        }
//        return pinPointingComplete;
//    }
//    //hlep me 
//    private bool FinishPatrol(StateController controller)
//    {
//        return controller.patrolCount == maxSearchLimit;  
//        //return controller.returnPoints.Count >= searchLimit; 
//        // if true, return to Search State 
//        // if false, remain in state 
//    }
//}
