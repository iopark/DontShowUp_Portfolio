//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SocialPlatforms;

//[CreateAssetMenu (fileName = "SearchCompleteDecision", menuName = "PluggableAI/Decisions/SearchComplete")]
//public class SearchCompleteDecision : Decision
//{
//    [SerializeField] public int searchLimit;  

//    public override bool Decide(StateController controller)
//    {
//        bool pinPointingComplete = Search(controller); 
//        return pinPointingComplete;
//    }

//    private bool Search(StateController controller)
//    {
//        return controller.returnPoints.Count >= searchLimit; 
//        //핀포인트를 다 찍었으면 다음 State는 패트롤로 넘어가는것이 정배다. 
//    }
//}
