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
//        //������Ʈ�� �� ������� ���� State�� ��Ʈ�ѷ� �Ѿ�°��� �����. 
//    }
//}
